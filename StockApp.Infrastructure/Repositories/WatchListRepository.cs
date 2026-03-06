
using StockApp.Domain.Entities.WatchLists;

namespace StockApp.Infrastructure.Repositories;

public class WatchListRepository : IWatchListRepository
{
	private readonly StockAppDbContext _db;

	public WatchListRepository(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task AddToWatchListAsync(WatchList watchList, CancellationToken cancellationToken)
	{
		_db.WatchLists.Add(watchList);
		await _db.SaveChangesAsync();
	}

	public async Task<bool> ExistsAsync(Guid userId, Guid stockId)
	{
		return await _db.WatchLists.AnyAsync(wl => wl.UserId == userId && wl.StockId == stockId);
	}

	public async Task<IEnumerable<WatchList>> GetAllAsync(Guid userId)
	{
		return await _db.WatchLists
			.AsNoTracking()
			.Where(wl => wl.UserId == userId)
			.Include(x => x.Stock)
			.ToListAsync();
	}
}
