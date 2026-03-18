namespace StockApp.Domain.Interfaces;

public interface IPortfolioRepository
{
	Task<Portfolio?> GetByUserAndStockAsync(Guid userId, Guid stockId, CancellationToken cancellationToken);
	Task AddAsync(Portfolio portfolio, CancellationToken cancellationToken);
	Task UpdateAsync(Portfolio portfolio, CancellationToken cancellationToken);
	Task<IEnumerable<Portfolio>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
	Task<int> GetTotalQuantityAsync(Guid userId, Guid stockId, CancellationToken cancellationToken);
}
