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
	private const int MaxPageSize = 100;

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

		// Kiểm tra bán khống
		if (dto.Direction == OrderDirection.Sell)
		{
			var availableQuantity = await _portfolioService.GetAvailableStockQuantityAsync(dto.UserId, dto.StockId, cancellationToken);
			if (availableQuantity < dto.Quantity)
				return Result<OrderDto>.Failure(OrderErrors.InsufficientStock);
		}

		// Tạo order
		var orderResult = dto.Type switch
		{
			OrderType.Market => Order.CreateMarketOrder(dto.UserId, dto.StockId, dto.Direction, dto.Quantity, dto.RequestedPrice),

			OrderType.Limit => dto.RequestedPrice.HasValue 
				? Order.CreateLimitOrder(dto.UserId, dto.StockId, dto.Direction, dto.Quantity, dto.RequestedPrice.Value) 
				: Result<Order>.Failure(OrderErrors.PriceRequired),

			OrderType.Stop => dto.RequestedPrice.HasValue 
				? Order.CreateStopOrder(dto.UserId, dto.StockId, dto.Direction, dto.Quantity, dto.RequestedPrice.Value) 
				: Result<Order>.Failure(OrderErrors.PriceRequired),

			_ => Result<Order>.Failure(OrderErrors.InvalidType)
		};

		if (orderResult.IsFailure) return Result<OrderDto>.Failure(orderResult.Errors);

		var order = orderResult.Value;

		await _orderRepo.AddAsync(order, cancellationToken);

		var createdOrder = await _orderRepo.GetByIdAsync(order.Id, cancellationToken);

		return Result<OrderDto>.Success(createdOrder!.ToOrderDto());
	}

	public async Task<Result<OrderDto>> ExecuteAsync(Guid orderId, CancellationToken cancellationToken)
	{
		var order = await _orderRepo.GetByIdAsync(orderId, cancellationToken);
		if (order is null)
			return Result<OrderDto>.Failure(OrderErrors.NotFound(orderId));

		var marketPrice = await _quoteService.GetCurrentPriceAsync(order.StockId, cancellationToken);
		if (marketPrice is null)
			return Result<OrderDto>.Failure(OrderErrors.MarketPriceUnavailable);

		var checkResult = order.CheckExecutionCondition(marketPrice.Value);
		if (checkResult.IsFailure)
			return Result<OrderDto>.Failure(checkResult.Errors);

		return await ExecuteOrderTransactionAsync(order, marketPrice.Value, cancellationToken);
	}

	private async Task<Result<OrderDto>> ExecuteOrderTransactionAsync(
		Order order,
		decimal marketPrice,
		CancellationToken cancellationToken)
	{
		var executedPrice = order.CalculateExecutionPrice(marketPrice);
		var executedQuantity = order.Quantity;

		try
		{
			var executeResult = order.MarkExecuted(executedPrice, executedQuantity);
			if (executeResult.IsFailure)
				return Result<OrderDto>.Failure(executeResult.Errors);

			await _unitOfWork.BeginTransactionAsync(cancellationToken);

			await _orderRepo.UpdateAsync(order, cancellationToken);

			var portfolioResult = await _portfolioService.ApplyOrderAsync(
				order.UserId, order.StockId, order.Direction, executedQuantity, executedPrice, cancellationToken);
			if (portfolioResult.IsFailure)
			{
				await _unitOfWork.RollbackTransactionAsync(CancellationToken.None);
				return Result<OrderDto>.Failure(portfolioResult.Errors);
			}

			var transactionResult = await _transactionService.ApplyOrderTransactionAsync(order, cancellationToken);
			if (transactionResult.IsFailure)
			{
				await _unitOfWork.RollbackTransactionAsync(CancellationToken.None);
				return Result<OrderDto>.Failure(transactionResult.Errors);
			}

			await _unitOfWork.CommitTransactionAsync(cancellationToken);
		}
		catch (Exception ex)
		{
			await _unitOfWork.RollbackTransactionAsync(CancellationToken.None);
			_logger.LogError(ex, $"Failed to execute order {order.Id}");
			return Result<OrderDto>.Failure(OrderErrors.ExecutionFailed);
		}

		_ = _notificationService.CreateOrderExecutedAsync(order, cancellationToken);

		return Result<OrderDto>.Success(order.ToOrderDto());
	}

	public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var order = await _orderRepo.GetByIdAsync(id, cancellationToken);
		return order?.ToOrderDto();
	}

	public async Task<PagedList<OrderDto>> GetByUserIdAsync(OrderQuery query, Guid userId, CancellationToken cancellationToken)
	{
		// PageSize không được quá MaxPageSize
		if (query.PageSize > MaxPageSize) 
			query.PageSize = MaxPageSize;

		var (orders, total) = await _orderRepo.GetByUserIdAsync(
			userId,
			query.StockId,
			query.Type,
			query.Direction,
			query.Statuses,
			query.FromDate,
			query.ToDate,
			query.SortBy,
			query.IsDescending,
			query.PageNumber,
			query.PageSize,
			cancellationToken
		);

		var dtos = orders.Select(o => o.ToOrderDto()).ToList();

		var pagedList = PagedList<OrderDto>.Create(dtos, query.PageNumber, query.PageSize, total);

		return pagedList;
	}

	public async Task<Result<OrderDto>> UpdateAsync(UpdateOrderDto dto, CancellationToken cancellationToken)
	{
		var order = await _orderRepo.GetByIdAsync(dto.OrderId, cancellationToken);
		if (order == null) 
			return Result<OrderDto>.Failure(OrderErrors.NotFound(dto.OrderId));

		if (order.UserId != dto.UserId)
			return Result<OrderDto>.Failure(OrderErrors.Forbidden);

		if (order.Status != OrderStatus.Pending)
			return Result<OrderDto>.Failure(OrderErrors.NotPending);

		if (order.Direction == OrderDirection.Sell && dto.Quantity > order.Quantity)
		{
			// nên cho vào 1 hàm khác
			var additionalQuantityNeeded = dto.Quantity - order.Quantity;
			var availableQuantity = await _portfolioService.GetAvailableStockQuantityAsync(
				order.UserId, order.StockId, cancellationToken);
			if (availableQuantity < additionalQuantityNeeded)
				return Result<OrderDto>.Failure(OrderErrors.InsufficientStock);
		}

		var updateResult = order.Update(dto.Quantity, dto.RequestedPrice);
		if (updateResult.IsFailure)
			return Result<OrderDto>.Failure(updateResult.Errors);

		await _orderRepo.UpdateAsync(order, cancellationToken);

		return Result<OrderDto>.Success(order.ToOrderDto());
	}
}
