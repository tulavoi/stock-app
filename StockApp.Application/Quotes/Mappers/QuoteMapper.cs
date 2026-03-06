namespace StockApp.Application.Quotes.Mappers;

public static class QuoteMapper
{
	public static HistoricalQuoteDto ToHistoricalQuoteDto(this DailyQuoteAggregate quote)
	{
		return new HistoricalQuoteDto
		{
			StockId = quote.StockId,
			Close = quote.Close,
			Volume = quote.Volume,
			Date = quote.Date
		};
	}

	public static RealTimeQuoteDto ToRealTimeQuoteDto(this RealTimeQuote realTimeQuote)
	{
		return new RealTimeQuoteDto
		{
			Symbol = realTimeQuote.Symbol,
			CompanyName = realTimeQuote.CompanyName,
			Price = realTimeQuote.Price,
			Change = realTimeQuote.Change,
			PercentChange = realTimeQuote.PercentChange,
			Volume = realTimeQuote.Volume,
			Sector = realTimeQuote.Sector,
			Industry = realTimeQuote.Industry,
			MarketCap = realTimeQuote.MarketCap,
			TimeStamp = realTimeQuote.TimeStamp
		};
	}
}
