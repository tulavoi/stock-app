namespace StockApp.Domain.Errors;

public static class PhoneNumberErrors
{
	public static readonly Error Empty = Error.Validation(
		"PhoneNumber.Empty",
		"Phone number cannot be empty",
		"phoneNumber");

	public static readonly Error InvalidFormat = Error.Validation(
		"PhoneNumber.InvalidFormat",
		"Invalid phone number format",
		"phoneNumber");
}
