using StockApp.Domain.Entities.Users;

namespace StockApp.Domain.Entities.Transactions;

public class Transaction
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public TransactionType Type { get; set; }
	public decimal Amount { get; set; }
	public DateTime TransactionDate { get; set; }

	public User User { get; set; } = null!;

	public Transaction() { }

	private Transaction(Guid userId, TransactionType type, decimal amount)
	{
		Id = Guid.CreateVersion7();
		UserId = userId;
		Type = type;
		Amount = amount;
		TransactionDate = DateTime.UtcNow;
	}

	public static Result<Transaction> Create(Guid userId, TransactionType type, decimal amount)
	{
		if (userId == Guid.Empty)
			return Result<Transaction>.Failure(TransactionErrors.InvalidUser);

		if (amount <= 0)
			return Result<Transaction>.Failure(TransactionErrors.InvalidAmount);

		var transaction = new Transaction(userId, type, amount);
		return Result<Transaction>.Success(transaction);
	}
}
