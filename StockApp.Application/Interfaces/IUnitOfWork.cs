namespace StockApp.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
	Task BeginTransactionAsync(CancellationToken cancellationToken);
	Task SaveChangesAsync(CancellationToken cancellationToken);
	Task CommitTransactionAsync(CancellationToken cancellationToken);
	Task RollbackTransactionAsync(CancellationToken cancellationToken);
}
