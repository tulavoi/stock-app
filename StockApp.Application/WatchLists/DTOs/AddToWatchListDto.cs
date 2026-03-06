namespace StockApp.Application.WatchLists.DTOs;

public class AddToWatchListDto
{
	public Guid UserId { get; set; }
	public Guid StockId { get; set; }
}
