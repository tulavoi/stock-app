namespace StockApp.Domain.Errors;

public static class HashedPasswordErrors
{
	public static readonly Error Empty = Error.Validation(
		"HashedPassword.Empty",
		"Hashed Password cannot be empty",
		"hashedPassword");
}
