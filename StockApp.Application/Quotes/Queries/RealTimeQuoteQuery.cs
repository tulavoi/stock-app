namespace StockApp.Application.Quotes.Queries;

public class RealTimeQuoteQuery
{
	public bool IsDecsending { get; set; } = false;
	public int PageNumber { get; set; } = 1;
	public int PageZise { get; set; } = 20;
	public string? Sector { get; set; }
	public string? Industry { get; set; }
}
