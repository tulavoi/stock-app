namespace StockApp.Domain.Entities.Quotes;

public static class QuoteErrors
{
	public static Error NotFound(Guid stockId) 
		=> Error.NotFound("Quote.NotFound", $"Quote for stock with ID '{stockId}' was not found.");

	public static readonly Error InvalidHistoryDays = Error.Validation("Quote.InvalidHistoryDays", "Days must be greater than zero.");
}
