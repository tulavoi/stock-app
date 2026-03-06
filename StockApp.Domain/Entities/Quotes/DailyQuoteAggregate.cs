namespace StockApp.Domain.Entities.Quotes;

public class DailyQuoteAggregate
{
	public Guid StockId { get; set; }
	public DateTime Date { get; set; }
	public decimal Close { get; set; }
	public long Volume { get; set; }
}
