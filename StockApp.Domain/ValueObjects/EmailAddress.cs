namespace StockApp.Domain.ValueObjects;

public class EmailAddress : ValueObject
{
	public string Value {  get; }

	private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

	private EmailAddress(string value)
	{
		Value = value;
	}

	public static Result<EmailAddress> Create(string raw)
	{
		if (string.IsNullOrWhiteSpace(raw))
			return Result<EmailAddress>.Failure(EmailErrors.Empty);

		var errors = new List<Error>();
		var normalized = raw.Trim();
		if (!EmailRegex.IsMatch(normalized))
			errors.Add(EmailErrors.InvalidFormat);

		return errors.Any()
			? Result<EmailAddress>.Failure(errors)
			: Result<EmailAddress>.Success(new EmailAddress(normalized));
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
