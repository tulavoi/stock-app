namespace StockApp.Application.Portfolios.Mappers;

public static class PortfolioMapper
{
	public static PortfolioDto ToPortfolioDto(this Portfolio portfolio)
	{
		return new PortfolioDto
		{
			StockId = portfolio.StockId,
			Symbol = portfolio.Stock.Symbol,
			CompanyName = portfolio.Stock.CompanyName,
			Quantity = portfolio.Quantity,
			AvaragePrice = portfolio.AvaragePrice,
			UpdatedAt = portfolio.UpdatedAt
		};
	}
}
