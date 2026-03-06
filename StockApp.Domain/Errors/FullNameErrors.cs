namespace StockApp.Domain.Errors;

public static class FullNameErrors
{
	public static readonly Error Empty = Error.Validation(
		"FullName.Empty",
		"Full Name cannot be empty",
		"fullName");

	public static readonly Error TooLong = Error.Validation(
		"FullName.TooLong",
		"Full Name is too long",
		"fullName");
}
