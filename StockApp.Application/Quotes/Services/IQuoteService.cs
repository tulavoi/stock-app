namespace StockApp.Application.Quotes.Services;

public interface IQuoteService
{
	Task<IEnumerable<RealTimeQuoteDto>> GetRealTimeQuoteAsync(RealTimeQuoteQuery query, CancellationToken cancellationToken = default);
	Task<Result<IEnumerable<HistoricalQuoteDto>>> GetHistoricalQuotesAsync(HistoricalQuoteQuery query, CancellationToken cancellationToken = default);
	Task<decimal?> GetCurrentPriceAsync(Guid stockId, CancellationToken cancellationToken = default);
}
