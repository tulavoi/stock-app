namespace StockApp.API.WatchLists.Responses;

public class WatchListResponse
{
	public Guid StockId { get; set; }
	public string Symbol { get; set; } = null!;
	public string CompanyName { get; set; } = null!;
}
