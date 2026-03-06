namespace StockApp.Domain.Entities.Quotes;

public class RealTimeQuote
{
	public long Id { get; set; }
	public string Symbol { get; set; } = default!;
	public string CompanyName { get; set; } = default!;
	public string IndexName { get; set; } = default!;
	public string IndexSymbol { get; set; } = default!;
	public decimal? MarketCap { get; set; }
	public string? SectorEn { get; set; }
	public string? IndustryEn { get; set; }
	public string? Sector { get; set; }
	public string? Industry { get; set; }
	public string StockType { get; set; } = default!;
	public decimal Price { get; set; }
	public decimal Change { get; set; }
	public decimal PercentChange { get; set; }
	public int Volume { get; set; }
	public DateTime TimeStamp { get; set; }
}
