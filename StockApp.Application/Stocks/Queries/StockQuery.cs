namespace StockApp.Application.Stocks.Queries;

public class StockQuery : BaseQuery
{
	public string? Sector { get; set; }
	public string? Industry { get; set; }

	public StockQuery()
	{
		SortBy = "Symbol";
	}
}
