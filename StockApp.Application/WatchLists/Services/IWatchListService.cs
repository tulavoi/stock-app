namespace StockApp.Application.WatchLists.Services;

public interface IWatchListService
{
	Task<IEnumerable<WatchListDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
	Task<Result<bool>> AddToWatchListAsync(AddToWatchListDto dto, CancellationToken cancellationToken);
}
