namespace StockApp.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
	Task BeginTransactionAsync();
	Task SaveChangesAsync();
	Task CommitTransactionAsync();
	Task RollbackTransactionAsync();
}
