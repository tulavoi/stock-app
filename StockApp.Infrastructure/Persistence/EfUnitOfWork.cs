namespace StockApp.Infrastructure.Persistence;

public class EfUnitOfWork : IUnitOfWork
{
	private readonly StockAppDbContext _db;
	private IDbContextTransaction? _transaction;

	public EfUnitOfWork(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task BeginTransactionAsync()
	{
		if (_transaction != null) return;
		_transaction = await _db.Database.BeginTransactionAsync();
	}

	public async Task CommitTransactionAsync()
	{
		if (_transaction == null) return;

		await _db.SaveChangesAsync();
		await _transaction.CommitAsync();

		await _transaction.DisposeAsync();
		_transaction = null;
	}

	public async Task RollbackTransactionAsync()
	{
		if (_transaction == null) return;

		await _transaction.RollbackAsync();
		await _transaction.DisposeAsync();
		_transaction = null;
	}

	public async Task SaveChangesAsync()
	{
		await _db.SaveChangesAsync();
	}

	public void Dispose()
	{
		_transaction?.Dispose();
	}
}
