namespace StockApp.Application.Quotes.Services;

public class QuoteService : IQuoteService
{
	private readonly IQuoteRepository _quoteRepo;
	private readonly IStockService _stockService;
 
	public QuoteService(IQuoteRepository quoteRepo, IStockService stockService)
	{
		_quoteRepo = quoteRepo;
		_stockService = stockService;
	}

	public async Task<decimal?> GetCurrentPriceAsync(Guid stockId, CancellationToken cancellationToken = default)
	{
		var quote = await _quoteRepo.GetLatestQuoteAsync(stockId, cancellationToken);

		return quote?.Price;
	}

	public async Task<Result<IEnumerable<HistoricalQuoteDto>>> GetHistoricalQuotesAsync(HistoricalQuoteQuery query, CancellationToken cancellationToken = default)
	{
		// Validate query
		if (query.Days <= 0) 
			return Result<IEnumerable<HistoricalQuoteDto>>.Failure(QuoteErrors.InvalidHistoryDays);

		var stockResult = await _stockService.GetByIdAsync(query.StockId, cancellationToken);
		if (stockResult.IsFailure)
			return Result<IEnumerable<HistoricalQuoteDto>>.Failure(stockResult.Errors);

		var aggregatedQuotes = await _quoteRepo.GetHistoricalQuotesAsync(query.Days, query.StockId, cancellationToken);
		var dtos = aggregatedQuotes.Select(q => q.ToHistoricalQuoteDto());

		return Result<IEnumerable<HistoricalQuoteDto>>.Success(dtos);
	}

	public async Task<IEnumerable<RealTimeQuoteDto>> GetRealTimeQuoteAsync(RealTimeQuoteQuery query, CancellationToken cancellationToken = default)
	{
		var quotes = await _quoteRepo.GetRealTimeQuotesAsync(
			query.IsDecsending, 
			query.PageNumber,
			query.PageZise,
			query.Sector,
			query.Industry,
			cancellationToken
		);

		return quotes.Select(q => q.ToRealTimeQuoteDto());
	}
}
