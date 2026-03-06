namespace StockApp.Application.Portfolios.Errors;

public static class PortfolioErrors
{
	public static readonly Error NotFound = Error.NotFound("Portfolio.NotFound", $"Portfolio not found.");

	public static readonly Error InvalidQuantity = Error.Validation("Portfolio.InvalidQuantity", $"Quantity must be greater than 0.", "quantity");

	public static readonly Error InvalidPrice = Error.Validation("Portfolio.InvalidPrice", $"Price must be greater than 0.", "price");

	public static Error InsufficientQuantity(Guid stockId) 
		=> Error.Validation("Portfolio.InsufficientQuantity", $"Insufficient quantity for stock {stockId} to execute order.");
}
