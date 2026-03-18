namespace StockApp.API.Portfolios.Mappers;

public static class PortfolioMapper
{
	public static PortfolioResponse	ToPortfolioResponse(this PortfolioDto dto)
	{
		return new PortfolioResponse
		{
			StockId = dto.StockId,
			Symbol = dto.Symbol,
			CompanyName = dto.CompanyName,
			Quantity = dto.Quantity,
			AvaragePrice = dto.AvaragePrice,
			UpdatedAt = dto.UpdatedAt
		};
	}
}
