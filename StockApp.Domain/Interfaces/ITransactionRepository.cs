namespace StockApp.Domain.Interfaces;

public interface ITransactionRepository
{
	Task AddAsync(Transaction transaction, CancellationToken cancellationToken);
}