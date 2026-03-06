using StockApp.Domain.Entities.Orders;

namespace StockApp.Application.Transactions.Services;

public interface ITransactionService
{
	Task<Result> ApplyOrderTransactionAsync(Guid userId, Order order);
}
