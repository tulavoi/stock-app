namespace StockApp.Domain.Entities.Users;

public static class UserErrors
{
	public static Error NotFound(Guid userId) 
		=> Error.NotFound("User.NotFound", $"User with ID '{userId}' was not found.");

	public static Error NotFoundByEmail(string email) 
		=> Error.NotFound("User.NotFoundByEmail", $"User with email '{email}' was not found.");

	public static Error NotFoundByUsername(string username)
		=> Error.NotFound("User.NotFoundByUsername", $"User with username '{username}' was not found.");

	public static Error EmailNotUnique(string email)
		=> Error.Conflict("User.EmailNotUnique", $"The email '{email}' is not unique.", "email");

	public static Error UsernameNotUnique(string username) 
		=> Error.Conflict("User.UsernameNotUnique", $"The username '{username}' is not unique.", "username");
}
