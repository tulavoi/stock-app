namespace StockApp.Infrastructure.Repositories;

public class PortfolioRepository : IPortfolioRepository
{
	private readonly StockAppDbContext _db;

	public PortfolioRepository(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task AddAsync(Portfolio portfolio, CancellationToken cancellationToken)
	{
		_db.Portfolios.Add(portfolio);
		await _db.SaveChangesAsync(cancellationToken);
	}

	public async Task<Portfolio?> GetByUserAndStockAsync(Guid userId, Guid stockId,	CancellationToken cancellationToken)
	{
		return await _db.Portfolios
			.AsNoTracking()
			.FirstOrDefaultAsync(p => p.UserId == userId && p.StockId == stockId, cancellationToken);
	}

	public async Task<IEnumerable<Portfolio>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
	{
		return await _db.Portfolios
			.AsNoTracking()
			.Include(p => p.Stock)
			.Where(p => p.UserId == userId)
			.ToListAsync(cancellationToken);
	}

	public async Task<int> GetTotalQuantityAsync(Guid userId, Guid stockId, CancellationToken cancellationToken)
	{
		var portfolio = await _db.Portfolios
			.FirstOrDefaultAsync(p => p.UserId == userId && p.StockId == stockId, cancellationToken);

		return portfolio?.Quantity ?? 0;
	}

	public async Task UpdateAsync(Portfolio portfolio, CancellationToken cancellationToken)
	{
		_db.Portfolios.Update(portfolio);
		await _db.SaveChangesAsync(cancellationToken);
	}
}
