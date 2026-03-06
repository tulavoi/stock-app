using StockApp.Domain.Errors;

namespace StockApp.Domain.ValueObjects;

public class FullName : ValueObject
{
	public string Value { get; }

	public FullName(string value)
	{
		Value = value;
	}

	public static Result<FullName> Create(string rawFullName)
	{
		if (string.IsNullOrWhiteSpace(rawFullName))
			return Result<FullName>.Failure(FullNameErrors.Empty);

		var errors = new List<Error>();
		var normalized = rawFullName.Trim();

		if (normalized.Length > 100)
			errors.Add(FullNameErrors.TooLong);

		return errors.Any()
			? Result<FullName>.Failure(errors)
			: Result<FullName>.Success(new FullName(normalized));
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
