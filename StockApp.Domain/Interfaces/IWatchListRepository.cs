using StockApp.Domain.Entities.WatchLists;

namespace StockApp.Domain.Interfaces;

public interface IWatchListRepository
{
	Task AddToWatchListAsync(WatchList watchList, CancellationToken cancellationToken);
	Task<bool> ExistsAsync(Guid userId, Guid stockId);
	Task<IEnumerable<WatchList>> GetAllAsync(Guid userId);
}
