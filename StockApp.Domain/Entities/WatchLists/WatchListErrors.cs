namespace StockApp.Domain.Entities.WatchLists;

public static class WatchListErrors
{
	public static readonly Error AlreadyAdded = Error.Conflict(
		"WatchList.AlreadyAdded",
		"Stock is already in the watch list.");
}
