namespace StockApp.Application.WatchLists.Services;

public class WatchListService : IWatchListService
{
	private readonly IWatchListRepository _watchListRepo;
	private readonly IStockRepository _stockRepo;

	public WatchListService(IWatchListRepository watchListRep, IStockRepository stockRepo)
	{
		_watchListRepo = watchListRep;
		_stockRepo = stockRepo;
	}

	public async Task<Result<bool>> AddToWatchListAsync(AddToWatchListDto dto, CancellationToken cancellationToken)
	{
		var stock = await _stockRepo.GetByIdAsync(dto.StockId, cancellationToken);
		if (stock == null)
			return Result<bool>.Failure(StockErrors.NotFound(dto.StockId));

		var existing = await _watchListRepo.ExistsAsync(dto.UserId, dto.StockId);
		if (existing)
			return Result<bool>.Failure(WatchListErrors.AlreadyAdded);

		await _watchListRepo.AddToWatchListAsync(dto.ToWatchList(), cancellationToken);
		return Result<bool>.Success(true);
	}

	public async Task<IEnumerable<WatchListDto>> GetAllAsync(Guid userId)
	{
		var watchLists = await _watchListRepo.GetAllAsync(userId);
		return watchLists.Select(wl => wl.ToWatchListDto());
	}
}
