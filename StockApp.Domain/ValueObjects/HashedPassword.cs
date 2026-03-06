
using StockApp.Domain.Errors;

namespace StockApp.Domain.ValueObjects;

public class HashedPassword : ValueObject
{
	public string Value { get; }

	private HashedPassword(string value)
	{
		Value = value;
	}

	public static Result<HashedPassword> Create(string hashedPassword)
	{
		if (string.IsNullOrWhiteSpace(hashedPassword))
			return Result<HashedPassword>.Failure(HashedPasswordErrors.Empty);

		return Result<HashedPassword>.Success(new HashedPassword(hashedPassword));
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		throw new NotImplementedException();
	}
}
