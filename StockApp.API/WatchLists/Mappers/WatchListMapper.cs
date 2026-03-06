using StockApp.API.WatchLists.Responses;

namespace StockApp.API.WatchLists.Mappers;

public static class WatchListMapper
{
	public static AddToWatchListDto ToAddToWatchListDto(this AddToWatchListRequest request, Guid userId)
	{
		return new AddToWatchListDto
		{
			UserId = userId,
			StockId = request.StockId,
		};
	}

	public static WatchListResponse ToWatchListResponse(this WatchListDto dto)
	{
		return new WatchListResponse
		{
			StockId = dto.StockId,
			Symbol = dto.Symbol,
			CompanyName = dto.CompanyName,
		};
	}
}
