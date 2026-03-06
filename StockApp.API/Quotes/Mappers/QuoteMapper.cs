namespace StockApp.API.Quotes.Mappers;

public static class QuoteMapper
{
	public static HistoricalQuoteResponse ToHistoricalQuoteResponse(this HistoricalQuoteDto dto)
	{
		return new HistoricalQuoteResponse
		{
			StockId = dto.StockId,
			Close = dto.Close,
			Volume = dto.Volume,
			Date = dto.Date
		};
	}
}
