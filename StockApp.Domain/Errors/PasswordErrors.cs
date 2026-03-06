namespace StockApp.Domain.Errors;

public static class PasswordErrors
{
	public static readonly Error Empty = Error.Validation(
		"Password.Empty",
		"Password cannot be empty",
		"password");

	public static readonly Error TooShort = Error.Validation(
		"Password.TooShort",
		"Password must be at least 6 characters",
		"password");

	public static readonly Error MissingUppercase = Error.Validation(
		"Password.MissingUppercase",
		"Password must contain at least one uppercase letter",
		"password");

	public static readonly Error MissingLowercase = Error.Validation(
		"Password.MissingLowercase",
		"Password must contain at least one lowercase letter",
		"password");

	public static readonly Error MissingDigit = Error.Validation(
		"Password.MissingDigit",
		"Password must contain at least one number",
		"password");

	public static readonly Error MissingSpecialChar = Error.Validation(
		"Password.MissingSpecialChar",
		"Password must contain at least one special character",
		"password");
}
