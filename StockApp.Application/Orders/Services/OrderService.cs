namespace StockApp.Application.Orders.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepo;
	private readonly IPortfolioService _portfolioService;
	private readonly INotificationService _notificationService;
	private readonly ITransactionService _transactionService;
	private readonly IQuoteService _quoteService;
	private readonly ILogger<OrderService> _logger;
	private readonly IUnitOfWork _unitOfWork;

	public OrderService(IOrderRepository orderRepo,
		IPortfolioService portfolioService,
		INotificationService notificationService,
		ITransactionService transactionService,
		IQuoteService quoteService,
		ILogger<OrderService> logger,
		IUnitOfWork unitOfWork)
	{
		_orderRepo = orderRepo;
		_portfolioService = portfolioService;
		_notificationService = notificationService;
		_transactionService = transactionService;
		_quoteService = quoteService;
		_logger = logger;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<OrderDto>> CreateAsync(CreateOrderDto dto, CancellationToken cancellationToken)
	{
		if (dto == null) return Result<OrderDto>.Failure(Error.NullValue);

		var orderResult = dto.Type switch
		{ 
			OrderType.Market => Order.CreateMarketOrder(dto.UserId, dto.StockId, dto.Direction, dto.Quantity),
			OrderType.Limit => Order.CreateLimitOrder(dto.UserId, dto.StockId, dto.Direction, dto.Quantity, dto.Price ?? 0),
			OrderType.Stop => Order.CreateStopOrder(dto.UserId, dto.StockId, dto.Direction, dto.Quantity, dto.Price ?? 0),
			_ => Result<Order>.Failure(OrderErrors.InvalidType)
		};

		if (orderResult.IsFailure) return Result<OrderDto>.Failure(orderResult.Errors);

		var order = orderResult.Value;

		await _orderRepo.AddAsync(order);

		return Result<OrderDto>.Success(order.ToOrderDto());
	}

	public async Task<Result<OrderDto>> ExecuteAsync(Guid orderId, CancellationToken cancellationToken)
	{
		// Get order
		var order = await _orderRepo.GetByIdAsync(orderId);
		if (order is null)
			return Result<OrderDto>.Failure(OrderErrors.NotFound(orderId));

		// Get market price
		var marketPrice = await _quoteService.GetCurrentPriceAsync(order.StockId, cancellationToken);
		if (marketPrice is null)
			return Result<OrderDto>.Failure(OrderErrors.MarketPriceUnavailable);

		// Check execution: Kiểm tra có được phép khớp lệnh không
		var checkResult = order.CheckExecutionCondition(marketPrice.Value);
		if (checkResult.IsFailure)
			return Result<OrderDto>.Failure(checkResult.Errors);

		// Calculate executed values
		var executedPrice = order.CalculateExecutionPrice(marketPrice.Value);
		var executedQuantity = order.Quantity;

		try
		{
			// Update order
			var executeResult = order.MarkExecuted(executedPrice, executedQuantity);
			if (executeResult.IsFailure)
				return Result<OrderDto>.Failure(executeResult.Errors);

			// Begin transaction to ensure data consistency across order, portfolio, and transaction updates
			await _unitOfWork.BeginTransactionAsync();

			await _orderRepo.UpdateAsync(order);

			// Update portfolio
			var portfolioResult = await _portfolioService.ApplyOrderAsync(
				order.UserId, order.StockId, order.Direction, executedQuantity, executedPrice);
			if (portfolioResult.IsFailure)
			{
				await _unitOfWork.RollbackTransactionAsync();
				return Result<OrderDto>.Failure(portfolioResult.Errors);
			}

			// Save transaction
			var transactionResult = await _transactionService.ApplyOrderTransactionAsync(order.UserId, order);
			if (transactionResult.IsFailure)
			{
				await _unitOfWork.RollbackTransactionAsync();
				return Result<OrderDto>.Failure(transactionResult.Errors);
			}

			await _unitOfWork.CommitTransactionAsync();
		}
		catch (Exception ex)
		{
			await _unitOfWork.RollbackTransactionAsync();
			_logger.LogError(ex, $"Failed to execute order {order.Id} for user {order.UserId} stock {order.StockId}");
			return Result<OrderDto>.Failure(OrderErrors.ExecutionFailed);
		}

		// Create notification
		_ = _notificationService.CreateOrderExecutedAsync(order, cancellationToken);

		return Result<OrderDto>.Success(order.ToOrderDto());
	}

	public async Task<OrderDto?> GetByIdAsync(Guid id)
	{
		var order = await _orderRepo.GetByIdAsync(id);
		return order?.ToOrderDto();
	}
}
