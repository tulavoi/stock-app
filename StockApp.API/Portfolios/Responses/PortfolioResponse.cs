namespace StockApp.API.Portfolios.Responses;

public class PortfolioResponse
{
	public Guid StockId { get; set; }
	public string Symbol { get; set; } = string.Empty;
	public string CompanyName { get; set; } = string.Empty;
	public int Quantity { get; set; }
	public decimal AvaragePrice { get; set; }
	public DateTime UpdatedAt { get; set; }
}
