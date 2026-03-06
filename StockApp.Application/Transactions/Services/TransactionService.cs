using StockApp.Domain.Entities.Orders;
using StockApp.Domain.Entities.Transactions;

namespace StockApp.Application.Transactions.Services;

public class TransactionService : ITransactionService
{
	private readonly ITransactionRepository _transactionRepo;

	public TransactionService(ITransactionRepository transactionRepo)
	{
		_transactionRepo = transactionRepo;
	}

	public async Task<Result> ApplyOrderTransactionAsync(Guid userId, Order order)
	{
		var amount = order.Price * order.Quantity;

		var type = order.Direction == OrderDirection.Buy
			? TransactionType.Withdrawal
			: TransactionType.Deposit;

		var transactionResult = Transaction.Create(userId, type, amount!.Value);

		if (transactionResult.IsFailure) return Result.Failure(transactionResult.Errors);

		await _transactionRepo.AddAsync(transactionResult.Value);
		return Result.Success();
	}
}
