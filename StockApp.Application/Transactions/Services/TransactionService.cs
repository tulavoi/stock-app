namespace StockApp.Application.Transactions.Services;

public class TransactionService : ITransactionService
{
	private readonly ITransactionRepository _transactionRepo;

	public TransactionService(ITransactionRepository transactionRepo)
	{
		_transactionRepo = transactionRepo;
	}

	public async Task<Result> ApplyOrderTransactionAsync(Order order, CancellationToken cancellationToken)
	{
		if (!order.ExecutedPrice.HasValue)
			return Result.Failure(OrderErrors.ExecutedPriceRequired);

		var amount = order.ExecutedPrice.Value * order.Quantity;

		var type = order.Direction == OrderDirection.Buy
			? TransactionType.Withdrawal
			: TransactionType.Deposit;

		var transactionResult = Transaction.Create(order.UserId, type, amount);

		if (transactionResult.IsFailure) return Result.Failure(transactionResult.Errors);

		await _transactionRepo.AddAsync(transactionResult.Value, cancellationToken);
		return Result.Success();
	}
}
