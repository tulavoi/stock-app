namespace StockApp.Domain.Entities.Orders;

public static class OrderErrors
{
	public static Error NotFound(Guid id) => Error.NotFound(
		"Order.NotFound",
		$"Order with ID {id} was not found.");

	public static readonly Error CreationFailed = Error.Failure(
		"Order.CreationFailed",
		"Failed to create order due to an unexpected error.");

	public static readonly Error NotPending = Error.Conflict(
		"Order.NotPending",
		"Order is not in 'Pending' state and cannot be executed.",
		"status");

	public static readonly Error MarketPriceUnavailable = Error.Failure(
		"Order.MarketPriceUnavailable",
		"Unable to fetch current market price for the stock.");

	public static Error LimitConditionNotMet(decimal marketPrice, decimal? limitPrice, OrderDirection direction) {
		var operatorStr = direction == OrderDirection.Buy ? "less than or equal to" : "greater than or equal to";

		return Error.Validation(
			"Order.LimitConditionNotMet",
			$"Market price ({marketPrice}) is not {operatorStr} limit price ({limitPrice})");
	}

	public static Error StopConditionNotMet(decimal marketPrice, decimal? stopPrice, OrderDirection direction)
	{
		var operatorStr = direction == OrderDirection.Buy ? "higher than or equal to" : "lower than or equal to";

		return Error.Validation(
			"Order.StopConditionNotMet",
			$"Market price ({marketPrice}) has not reached stop price ({stopPrice}). yet ({operatorStr} required).");
	}

	public static readonly Error ExecutionFailed = Error.Failure(
		"Order.ExecutionFailed",
		"Failed to execute order due to an internal error.");

	public static readonly Error InvalidType = Error.Validation(
		"Order.InvalidType",
		"Unknown order type.");

	public static readonly Error InvalidQuantity = Error.Validation(
		"Order.InvalidQuantity",
		"Quantity must be greater than 0.",
		"quantity");

	public static readonly Error InvalidPrice = Error.Validation(
		"Order.InvalidPrice", 
		"Price must be greater than 0",
		"price");

	public static readonly Error PriceRequired = Error.Validation(
		"Order.PriceRequired", 
		"Price is required for Limit/Stop orders");

	public static readonly Error PriceNotAllowed = Error.Validation(
		"Order.PriceNotAllowed", "Market orders should not have a specific price");

	public static readonly Error InvalidExecution = Error.Validation(
		"Order.InvalidExecution", "Cannot execute an order with invalid parameters or state");

	public static readonly Error AlreadyExecuted = Error.Conflict(
		"Order.AlreadyExecuted",
		"Order has already been executed and cannot be executed again.");
}
