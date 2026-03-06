namespace StockApp.Application.Stocks.Services;

public class StockService : IStockService
{
	private readonly IStockRepository _stockRepo;

	public StockService(IStockRepository stockRepo)
	{
		_stockRepo = stockRepo;
	}

	public async Task<PagedList<StockDto>> GetAllAsync(StockQuery query, CancellationToken cancellationToken)
	{
		var (stocks, totalCount) = await _stockRepo.GetAllAsync(
			cancellationToken,
			query.IsDecsending, 
			query.SortBy, 
			query.PageNumber, 
			query.PageSize, 
			query.Sector, 
			query.Industry, 
			query.Keyword);

		var dtos = stocks.Select(s => s.ToStockDto()).ToList();

		var pagedList = PagedList<StockDto>.Create(dtos, query.PageNumber, query.PageSize, totalCount);

		return pagedList;
	}

	public async Task<Result<StockDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var stock = await _stockRepo.GetByIdAsync(id, cancellationToken);

		if (stock is null) return Result<StockDto>.Failure(StockErrors.NotFound(id));

		return Result<StockDto>.Success(stock.ToStockDto());
	}
}
