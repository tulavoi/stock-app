using StockApp.Domain.Entities.Quotes;

namespace StockApp.Domain.Interfaces;

public interface IQuoteRepository
{
	Task<IEnumerable<RealTimeQuote>> GetRealTimeQuotesAsync(
		bool isDescending,
		int pageNumber,
		int pageSize,
		string? sector,
		string? industry,
		CancellationToken cancellationToken = default
	);

	Task<IEnumerable<DailyQuoteAggregate>> GetHistoricalQuotesAsync(int days, Guid stockId, CancellationToken cancellationToken = default);
	Task<Quote?> GetLatestQuoteAsync(Guid stockId, CancellationToken cancellationToken = default);
}
