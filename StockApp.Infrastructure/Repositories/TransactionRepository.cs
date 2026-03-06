
using StockApp.Domain.Entities.Transactions;

namespace StockApp.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
	private readonly StockAppDbContext _db;

	public TransactionRepository(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task AddAsync(Transaction transaction)
	{
		_db.Transactions.Add(transaction);
		await _db.SaveChangesAsync();
	}
}
