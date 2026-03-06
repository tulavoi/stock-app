namespace StockApp.Domain.Errors;

public static class AuthenticationErrors
{
	public static readonly Error InvalidCredentials = Error.Unauthorized(
		"Auth.InvalidCredentials",
		"Invalid email or password."
	);

	public static readonly Error InvalidToken = Error.Unauthorized(
		"Auth.InvalidToken",
		"The token is invalid");
}
