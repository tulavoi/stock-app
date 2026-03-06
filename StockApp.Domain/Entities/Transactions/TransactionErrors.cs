namespace StockApp.Domain.Entities.Transactions;

public static class TransactionErrors
{
	public static readonly Error InvalidUser = Error.Validation("Transaction.InvalidUser", "User identifier cannot be empty.", "userId");

	public static readonly Error InvalidAmount = Error.Validation("Transaction.InvalidAmount", "Transaction amount must be greater than 0.", "amount");
}
