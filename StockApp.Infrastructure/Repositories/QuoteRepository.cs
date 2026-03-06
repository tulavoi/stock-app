using StockApp.Domain.Entities.Quotes;

namespace StockApp.Infrastructure.Repositories;

public class QuoteRepository : IQuoteRepository
{
	private readonly StockAppDbContext _db;

	public QuoteRepository(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<DailyQuoteAggregate>> GetHistoricalQuotesAsync(int days, Guid stockId, CancellationToken cancellationToken = default)
	{
		var now = DateTime.UtcNow.Date;
		var fromDate = now.AddDays(-days);
		var toDate = now.AddDays(1);

		return await _db.Quotes
			.AsNoTracking()
			.Where(q => q.TimeStamp >= fromDate
				&& q.TimeStamp <= toDate
				&& q.StockId == stockId)
			.GroupBy(q => q.TimeStamp.Date)
			.OrderBy(g => g.Key)
			.Select(g => new DailyQuoteAggregate
			{
				StockId = stockId,
				Date = g.Key,
				Close = g.Average(x => x.Price),
				Volume = g.Sum(x => x.Volume),
			})
			.ToListAsync(cancellationToken);
	}

	public async Task<Quote?> GetLatestQuoteAsync(Guid stockId, CancellationToken cancellationToken = default)
	{
		return await _db.Quotes
			.AsNoTracking()
			.Where(q => q.StockId == stockId)
			.OrderByDescending(q => q.TimeStamp)
			.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<IEnumerable<RealTimeQuote>> GetRealTimeQuotesAsync(bool isDescending, 
		int pageNumber, 
		int pageSize, 
		string? sector, 
		string? industry,
		CancellationToken cancellationToken = default)
	{
		var data = _db.RealTimeQuotes
			.AsNoTracking()
			.AsQueryable();

		if (!string.IsNullOrEmpty(sector))
			data = data.Where(q => q.Sector == sector);

		if (!string.IsNullOrEmpty(industry))
			data = data.Where(q => q.Industry == industry);

		data = isDescending ? data.OrderByDescending(x => x.PercentChange) 
			: data.OrderBy(x => x.PercentChange);

		return await data
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);
	}
}
