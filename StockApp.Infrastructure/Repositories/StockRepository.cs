using StockApp.Domain.Entities.Stocks;

namespace StockApp.Infrastructure.Repositories;

public class StockRepository : IStockRepository
{
	private readonly StockAppDbContext _db;
	 
	public StockRepository(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task<(IEnumerable<Stock> items, long totalCount)> GetAllAsync(
		CancellationToken cancellationToken,
		bool isDescending,
		string sortBy,
		int pageNumber,
		int pageSize,
		string? sector = null,
		string? industry = null,
		string? keyword = null)
	{
		var query = _db.Stocks.AsNoTracking().AsQueryable();

		// Filtering
		if (!string.IsNullOrEmpty(sector))
			query = query.Where(s => s.Sector == sector);

		if (!string.IsNullOrEmpty(industry))
			query = query.Where(s => s.Industry == industry);

		// Searching
		if (!string.IsNullOrEmpty(keyword))
		{
			query = query.Where(s =>
				EF.Functions.Collate(s.Symbol, "SQL_Latin1_General_CP1_CI_AI").Contains(keyword) ||
				EF.Functions.Collate(s.CompanyName, "SQL_Latin1_General_CP1_CI_AI").Contains(keyword) ||
				EF.Functions.Collate(s.Sector ?? "", "SQL_Latin1_General_CP1_CI_AI").Contains(keyword) ||
				EF.Functions.Collate(s.Industry, "SQL_Latin1_General_CP1_CI_AI").Contains(keyword));
		}

		// Sorting
		query = sortBy?.ToLower() switch
		{
			"symbol" => isDescending ? query.OrderByDescending(s => s.Symbol) : query.OrderBy(s => s.Symbol),
			"companyname" => isDescending ? query.OrderByDescending(s => s.CompanyName) : query.OrderBy(s => s.CompanyName),
			"marketcap" => isDescending ? query.OrderByDescending(s => s.MarketCap) : query.OrderBy(s => s.MarketCap),
			_ => query.OrderBy(s => s.Symbol) // default
		};

		var totalCount = await query.LongCountAsync(cancellationToken);

		// Pagination
		var items = await query
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		return (items, totalCount);
	}

	public async Task<Stock?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await _db.Stocks.FindAsync(id);
	}
}
