using StockApp.Domain.Entities.Stocks;
using System.Threading;

namespace StockApp.Domain.Interfaces;

public interface IStockRepository
{
	Task<(IEnumerable<Stock> items, long totalCount)> GetAllAsync(
		CancellationToken cancellationToken,
		bool isDescending,
		string sortBy,
		int pageNumber,
		int pageSize,
		string? sector = null,
		string? industry = null,
		string? keyword = null);

	Task<Stock?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
