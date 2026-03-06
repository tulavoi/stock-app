namespace StockApp.Domain.Entities.Orders;

public class Order
{
	public Guid Id { get; private set; }
	public Guid UserId { get; private set; }
	public Guid StockId { get; private set; }

	public OrderType Type { get; private set; }
	public OrderDirection Direction { get; private set; }
	public OrderStatus Status { get; private set; }

	public int Quantity { get; private set; }
	public int ExecuteQuantity { get; private set; }
	public decimal? Price { get; private set; }
	public decimal? ExecutePrice { get; private set; }
	public DateTime OrderDate { get; private set; }
	public DateTime? ExecutedDate { get; private set; }

	public User User { get; private set; } = null!;
	public Stock Stock { get; private set; } = null!;

	private Order() { }

	private Order(Guid userId, Guid stockId, OrderType type,
				 OrderDirection direction, int quantity, decimal? price)
	{
		Id = Guid.CreateVersion7();
		UserId = userId;
		StockId = stockId;
		Type = type;
		Direction = direction;
		Quantity = quantity;
		Price = price;
		Status = OrderStatus.Pending;
		OrderDate = DateTime.UtcNow;
	}

	public static Result<Order> CreateMarketOrder(Guid userId, Guid stockId, OrderDirection direction, int quantity)
	{
		if (quantity <= 0) return Result<Order>.Failure(OrderErrors.InvalidQuantity);

		var order = new Order(userId, stockId, OrderType.Market, direction, quantity, price: null);

		return Result<Order>.Success(order);
	}

	public static Result<Order> CreateLimitOrder(Guid userId, Guid stockId, OrderDirection direction, int quantity, decimal price)
	{
		if (quantity <= 0) return Result<Order>.Failure(OrderErrors.InvalidQuantity);

		if (price <= 0) return Result<Order>.Failure(OrderErrors.InvalidPrice);

		var order = new Order(userId, stockId, OrderType.Market, direction, quantity, price: price);

		return Result<Order>.Success(order);
	}

	public static Result<Order> CreateStopOrder(Guid userId, Guid stockId, OrderDirection direction, int quantity, decimal stopPrice)
	{
		if (quantity <= 0) return Result<Order>.Failure(OrderErrors.InvalidQuantity);

		if (stopPrice <= 0) return Result<Order>.Failure(OrderErrors.InvalidPrice);

		var order = new Order(userId, stockId, OrderType.Market, direction, quantity, price: stopPrice);

		return Result<Order>.Success(order);
	}

	public Result MarkExecuted(decimal executedPrice, int executedQuantity)
	{
		if (Status == OrderStatus.Executed)
			return Result.Failure(OrderErrors.AlreadyExecuted);

		if (executedQuantity <= 0 || executedQuantity > Quantity)
			return Result.Failure(OrderErrors.InvalidExecution);

		if (executedPrice <= 0)
			return Result.Failure(OrderErrors.InvalidPrice);

		ExecutePrice = executedPrice;
		ExecuteQuantity = executedQuantity;
		Status = OrderStatus.Executed;
		ExecutedDate = DateTime.UtcNow;

		return Result.Success();
	}

	public Result CheckExecutionCondition(decimal currMarketPrice)
	{
		if (Status != OrderStatus.Pending)
			return Result.Failure(OrderErrors.NotPending);

		return Type switch
		{
			OrderType.Market => Result.Success(),
			OrderType.Limit => CheckLimitCondition(currMarketPrice),
			OrderType.Stop => CheckStopCondition(currMarketPrice),
			_ => Result.Failure(OrderErrors.InvalidType),
		};
	}

	private Result CheckStopCondition(decimal marketPrice)
	{
		if (Direction == OrderDirection.Buy && marketPrice >= Price)
			return Result.Success();

		if (Direction == OrderDirection.Sell && marketPrice <= Price)
			return Result.Success();

		return Result.Failure(OrderErrors.StopConditionNotMet(marketPrice, Price, Direction));
	}

	private Result CheckLimitCondition(decimal marketPrice)
	{
		if (Direction == OrderDirection.Buy && marketPrice <= Price)
			return Result.Success();

		if (Direction == OrderDirection.Sell && marketPrice >= Price)
			return Result.Success();

		return Result.Failure(OrderErrors.LimitConditionNotMet(marketPrice, Price, Direction));
	}

	public decimal CalculateExecutionPrice(decimal currMarketPrice)
	{
		return Type switch
		{
			// Market Order & Stop Order: Luôn khớp theo giá thị trường tại thời điểm đó
			OrderType.Market => currMarketPrice,
			OrderType.Stop => currMarketPrice,

			// Limit Order: Khớp theo giá có lợi nhất cho user
			OrderType.Limit => CalculateLimitExecutionPrice(currMarketPrice),
			_ => currMarketPrice,
		};
	}

	private decimal CalculateLimitExecutionPrice(decimal marketPrice)
	{
		if (Direction == OrderDirection.Buy)
			return Math.Min(marketPrice, Price!.Value);

		return Math.Max(marketPrice, Price!.Value);
	}

	//public List<string> Validate()
	//{
	//	var errors = new List<string>();

	//	if (UserId == Guid.Empty || StockId == Guid.Empty)
	//	{
	//		errors.Add("UserId and StockId are required");
	//	}
	//	if (Quantity <= 0)
	//	{
	//		errors.Add("Quantity must be greater than 0");
	//	}
	//	switch (Type)
	//	{
	//		case OrderType.Market:
	//			if (Price != null)
	//				errors.Add("Market order should not have a Price");
	//			break;

	//		case OrderType.Limit:
	//		case OrderType.Stop:
	//			if (Price is null)
	//				errors.Add($"{Type} order requires a Price");
	//			else if (Price.Value <= 0)
	//				errors.Add($"{Type} order requires Price > 0");
	//			break;
	//	}

	//	return errors;
	//}
}