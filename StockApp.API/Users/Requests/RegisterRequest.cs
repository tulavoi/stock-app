namespace StockApp.API.Users.Requests;

public class RegisterRequest
{
	public string Username { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Phone { get; set; } = null!;
	public string Fullname { get; set; } = null!;
	public DateOnly DateOfBirth { get; set; }
	public string? Country { get; set; }
	public string Password { get; set; } = null!;
}
