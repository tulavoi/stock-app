using StockApp.Domain.Entities.Portfolios;

namespace StockApp.Infrastructure.Repositories;

public class PortfolioRepository : IPortfolioRepository
{
	private readonly StockAppDbContext _db;

	public PortfolioRepository(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task AddAsync(Portfolio portfolio)
	{
		_db.Portfolios.Add(portfolio);
		await _db.SaveChangesAsync();
	}

	public async Task<Portfolio?> GetByUserAndStockAsync(Guid userId, Guid stockId)
	{
		return await _db.Portfolios
			.AsNoTracking()
			.FirstOrDefaultAsync(p => p.UserId == userId && p.StockId == stockId);
	}

	public async Task<IEnumerable<Portfolio>> GetByUserIdAsync(Guid userId)
	{
		return await _db.Portfolios
			.AsNoTracking()
			.Where(p => p.UserId == userId)
			.ToListAsync();
	}

	public async Task UpdateAsync(Portfolio portfolio)
	{
		_db.Portfolios.Update(portfolio);
		await _db.SaveChangesAsync();
	}
}
