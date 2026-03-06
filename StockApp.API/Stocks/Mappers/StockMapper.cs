namespace StockApp.API.Stocks.Mappers;

public static class StockMapper
{
	public static StockResponse ToStockResponse(this StockDto dto)
	{
		return new StockResponse
		{
			Id = dto.Id,
			Symbol = dto.Symbol,
			CompanyName = dto.CompanyName,
			Sector = dto.Sector ?? string.Empty,
			Industry = dto.Industry,
			MarketCap = dto.MarketCap,
			MarketCapDisplay = dto.MarketCap.HasValue
				? FormatMarketCap(dto.MarketCap.Value)
				: null,
			StockType = dto.StockType,
			Rank = dto.Rank,
			Reason = dto.Reason
		};
	}

	private static string FormatMarketCap(decimal value)
	{
		if (value >= 1_000_000_000) return $"{value / 1_000_000_000:F2}B";
		if (value >= 1_000_000) return $"{value / 1_000_000:F2}M";
		if (value >= 1_000) return $"{value / 1_000:F2}K";
		return value.ToString("F0");
	}
}
