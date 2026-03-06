namespace StockApp.Application.Users.DTOs;

public class RegisterDto
{
	public Guid Id { get; set; }

	public string Username { get; set; } = null!;

	public string Password { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string Phone { get; set; } = null!;

	public string FullName { get; set; } = null!;

	public DateOnly DateOfBirth { get; set; }

	public string? Country { get; set; }
}
