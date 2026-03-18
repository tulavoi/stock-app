namespace StockApp.Infrastructure.Persistence;

public class EfUnitOfWork : IUnitOfWork
{
	private readonly StockAppDbContext _db;
	private IDbContextTransaction? _transaction;

	public EfUnitOfWork(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task BeginTransactionAsync(CancellationToken cancellationToken)
	{
		if (_transaction != null) return;
		_transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
	}

	public async Task CommitTransactionAsync(CancellationToken cancellationToken)
	{
		if (_transaction == null) return;

		await _db.SaveChangesAsync(cancellationToken);
		await _transaction.CommitAsync(cancellationToken);

		await _transaction.DisposeAsync();
		_transaction = null;
	}

	public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
	{
		if (_transaction == null) return;

		await _transaction.RollbackAsync(cancellationToken);

		await _transaction.DisposeAsync();
		_transaction = null;
	}

	public async Task SaveChangesAsync(CancellationToken cancellationToken)
	{
		await _db.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		_transaction?.Dispose();
	}
}
