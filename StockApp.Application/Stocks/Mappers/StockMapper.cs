using StockApp.Domain.Entities.Stocks;

namespace StockApp.Application.Stocks.Mappers;

public static class StockMapper
{
	public static StockDto ToStockDto(this Stock stock)
	{
		return new StockDto
		{
			Id = stock.Id,
			Symbol = stock.Symbol,
			CompanyName = stock.CompanyName,
			MarketCap = stock.MarketCap,
			Sector = stock.Sector,
			Industry = stock.Industry,
			SectorEn = stock.SectorEn,
			IndustryEn = stock.IndustryEn,
			StockType = stock.StockType,
			Rank = stock.Rank,
			RankSource = stock.RankSource,
			Reason = stock.Reason
		};	
	}
}
