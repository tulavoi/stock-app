namespace StockApp.Domain.Errors;

public static class DateOfBirthErrors
{
	public static readonly Error CannotBeFuture = Error.Validation(
		"DateOfBirth.CannotBeFuture", 
		"Date of birth cannot be in the future.", 
		"dob"
	);

	public static readonly Error TooYoung = Error.Validation(
		"DateOfBirth.TooYoung", 
		"User must be at least 15 years old", 
		"dob"
	);
}
