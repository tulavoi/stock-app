namespace StockApp.Application.Quotes.DTOs;

public class HistoricalQuoteDto
{
	public Guid StockId { get; set; }
	public decimal Close { get; set; }
	public long Volume { get; set; }
	public DateTime Date { get; set; }
}
