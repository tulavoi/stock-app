namespace StockApp.Domain.Errors;

public static class UsernameErrors
{
	public static readonly Error Empty = Error.Validation(
		"Username.Empty",
		"Username cannot be empty",
		"username");

	public static readonly Error InvalidLength = Error.Validation(
		"Username.InvalidLength",
		"Username must be between 4 and 20 characters",
		"username");

	public static readonly Error InvalidFormat = Error.Validation(
		"Username.InvalidFormat",
		"Invalid username format",
		"username");
}
