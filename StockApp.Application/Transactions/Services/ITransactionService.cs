namespace StockApp.Application.Transactions.Services;

public interface ITransactionService
{
	Task<Result> ApplyOrderTransactionAsync(Order order, CancellationToken cancellationToken);
}
