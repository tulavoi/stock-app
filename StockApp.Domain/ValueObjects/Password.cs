using StockApp.Domain.Errors;

namespace StockApp.Domain.ValueObjects;

public class Password : ValueObject
{
	public string Value { get; }

	private Password(string value)
	{
		Value = value;
	}

	public static Result<Password> Create(string password)
	{
		if (string.IsNullOrWhiteSpace(password))
			return Result<Password>.Failure(PasswordErrors.Empty);

		var errors = new List<Error>();

		if (password.Length < 6)
			errors.Add(PasswordErrors.TooShort);

		//if (!Regex.IsMatch(password, @"[A-Z]"))
		//	errors.Add(PasswordErrors.MissingUppercase);

		//if (!Regex.IsMatch(password, @"[a-z]"))
		//	errors.Add(PasswordErrors.MissingLowercase);

		//if (!Regex.IsMatch(password, @"[0-9]"))
		//	errors.Add(PasswordErrors.MissingDigit);

		//if (!Regex.IsMatch(password, @"[\W_]"))
		//	errors.Add(PasswordErrors.MissingSpecialChar);

		return errors.Any()
			? Result<Password>.Failure(errors)
			: Result<Password>.Success(new Password(password));
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
}
