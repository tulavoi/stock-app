
using StockApp.Domain.Errors;

namespace StockApp.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
	public string Value { get; }
	private static readonly Regex PhoneRegex = new(@"^\d{10,11}$", RegexOptions.Compiled);

	private PhoneNumber(string value)
	{
		Value = value;
	}

	public static Result<PhoneNumber> Create(string rawPhoneNumber)
	{
		if (string.IsNullOrWhiteSpace(rawPhoneNumber))
			return Result<PhoneNumber>.Failure(PhoneNumberErrors.Empty);

		var errors = new List<Error>();
		var normalized = rawPhoneNumber.Trim();

		if (!PhoneRegex.IsMatch(normalized))
			errors.Add(PhoneNumberErrors.InvalidFormat);

		return errors.Any()
			? Result<PhoneNumber>.Failure(errors)
			: Result<PhoneNumber>.Success(new PhoneNumber(normalized));
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
