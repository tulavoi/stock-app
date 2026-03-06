namespace StockApp.Application.Quotes.DTOs;

public class RealTimeQuoteDto
{
	public string Symbol { get; set; } = default!;
	public string CompanyName { get; set; } = default!;
	public decimal Price { get; set; }
	public decimal Change { get; set; }
	public decimal PercentChange { get; set; }
	public int Volume { get; set; }
	public string? Sector { get; set; }
	public string? Industry { get; set; }
	public decimal? MarketCap { get; set; }
	public DateTime TimeStamp { get; set; }
}
