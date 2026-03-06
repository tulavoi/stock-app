namespace StockApp.API.Users.Responses;

public class UserResponse
{
	public Guid Id { get; set; }

	public string Username { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string Phone { get; set; } = null!;

	public string FullName { get; set; } = default!;

	public DateOnly? DateOfBirth { get; set; }

	public string? Country { get; set; }
}
