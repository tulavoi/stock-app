namespace StockApp.Domain.Errors;

public static class EmailErrors
{
	public static readonly Error Empty = Error.Validation(
		"Email.Empty", 
		"The email is empty.",
		"email"
	);

	public static readonly Error InvalidFormat = Error.Validation(
		"Email.InvalidFormat", 
		"The email format is invalid.", 
		"email"
	);

	public static readonly Error AlreadyInUse = Error.Conflict(
		"Email.AlreadyInUse", 
		"The email address is already in use.", 
		"email"
	);
}
