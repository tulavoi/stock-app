using StockApp.Domain.Entities.WatchLists;

namespace StockApp.Application.WatchLists.Mappers;

public static class WatchListMapper
{
	public static WatchList ToWatchList(this AddToWatchListDto dto)
	{
		return new WatchList
		{
			UserId = dto.UserId,
			StockId = dto.StockId
		};
	}

	public static WatchListDto ToWatchListDto(this WatchList watchList)
	{
		return new WatchListDto
		{
			StockId = watchList.StockId,
			Symbol = watchList.Stock?.Symbol ?? string.Empty,
			CompanyName = watchList.Stock?.CompanyName ?? string.Empty,
		};
	}
}
