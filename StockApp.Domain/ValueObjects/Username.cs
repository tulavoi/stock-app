namespace StockApp.Domain.ValueObjects;

public class Username : ValueObject
{
	public string Value { get; }

	private static readonly Regex UsernameRegex = new(@"^[a-zA-Z0-9_]+$", RegexOptions.Compiled);

	private Username(string value)
	{
		Value = value;
	}

	public static Result<Username> Create(string raw)
	{
		if (string.IsNullOrWhiteSpace(raw))
			return Result<Username>.Failure(UsernameErrors.Empty);

		var errors = new List<Error>();
		var normalized = raw.Trim();

		if (normalized.Length < 4 || normalized.Length > 20)
			errors.Add(UsernameErrors.InvalidLength);

		if (!UsernameRegex.IsMatch(normalized))
			errors.Add(UsernameErrors.InvalidFormat);

		return errors.Any()
			? Result<Username>.Failure(errors)
			: Result<Username>.Success(new Username(raw.Trim()));
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
