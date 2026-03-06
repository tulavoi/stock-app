using StockApp.Domain.Entities.Stocks;
using StockApp.Domain.Entities.Users;

namespace StockApp.Domain.Entities.WatchLists;

public class WatchList
{
	public Guid UserId { get; set; }
	public Guid StockId { get; set; }

	public User? User { get; set; }
	public Stock? Stock { get; set; }
}
