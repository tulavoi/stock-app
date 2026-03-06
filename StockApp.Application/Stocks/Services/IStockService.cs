namespace StockApp.Application.Stocks.Services;

public interface IStockService
{
	Task<Result<StockDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<PagedList<StockDto>> GetAllAsync(StockQuery query, CancellationToken cancellationToken);
}
