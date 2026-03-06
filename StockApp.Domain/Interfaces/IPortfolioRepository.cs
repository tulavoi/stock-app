using StockApp.Domain.Entities.Portfolios;

namespace StockApp.Domain.Interfaces;

public interface IPortfolioRepository
{
	Task<Portfolio?> GetByUserAndStockAsync(Guid userId, Guid stockId);
	Task AddAsync(Portfolio portfolio);
	Task UpdateAsync(Portfolio portfolio);
	Task<IEnumerable<Portfolio>> GetByUserIdAsync(Guid userId);
}
