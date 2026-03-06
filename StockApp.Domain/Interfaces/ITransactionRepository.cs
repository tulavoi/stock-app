using StockApp.Domain.Entities.Transactions;

namespace StockApp.Domain.Interfaces;

public interface ITransactionRepository
{
	Task AddAsync(Transaction transaction);
}
