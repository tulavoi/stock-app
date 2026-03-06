namespace StockApp.Application.Stocks.DTOs;

public class StockDto
{
	public Guid Id { get; set; }

	public string Symbol { get; set; } = null!;

	public string CompanyName { get; set; } = null!;

	public decimal? MarketCap { get; set; }

	public string? Sector { get; set; }

	public string Industry { get; set; } = null!;

	public string? SectorEn { get; set; }

	public string? IndustryEn { get; set; }

	public string? StockType { get; set; }

	public int Rank { get; set; } = 0;

	public string? RankSource { get; set; }

	public string? Reason { get; set; }

}
