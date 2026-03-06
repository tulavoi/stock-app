namespace StockApp.Application.WatchLists.Services;

public interface IWatchListService
{
	Task<IEnumerable<WatchListDto>> GetAllAsync(Guid userId);
	Task<Result<bool>> AddToWatchListAsync(AddToWatchListDto dto, CancellationToken cancellationToken);
}
