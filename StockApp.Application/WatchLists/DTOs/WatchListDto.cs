namespace StockApp.Application.WatchLists.DTOs;

public class WatchListDto
{
	public Guid StockId { get; set; }
	public string Symbol { get; set; } = null!;
	public string CompanyName { get; set; } = null!;
}
