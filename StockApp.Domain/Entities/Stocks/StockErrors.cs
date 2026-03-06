namespace StockApp.Domain.Entites.Stocks;

public static class StockErrors
{
	public static Error NotFound(Guid id) => Error.NotFound(
		"Stock.NotFound",
		$"Stock with ID '{id}' was not found.");
}
