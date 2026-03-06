namespace StockApp.API.Stocks.Responses;

public class StockResponse
{
	public Guid Id { get; set; }

	public string Symbol { get; set; } = null!;

	public string CompanyName { get; set; } = null!;

	public string Sector { get; set; } = null!;

	public string Industry { get; set; } = null!;

	public decimal? MarketCap { get; set; }

	public string? MarketCapDisplay { get; set; }

	public string? StockType { get; set; }

	public int? Rank { get; set; }

	public string? Reason { get; set; }
}
