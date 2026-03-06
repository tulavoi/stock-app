namespace StockApp.Application.Quotes.Queries;

public class HistoricalQuoteQuery
{
	public int Days { get; set; } = 10;
	public Guid StockId { get; set; }
}
