using StockApp.Domain.Entities.WatchLists;

namespace StockApp.Domain.Entities.Users;

public class User
{
	public Guid Id { get; private set; }

	public Username Username { get; private set; } = null!;

	public HashedPassword HashedPassword { get; private set; } = null!;

	public EmailAddress Email { get; private set; } = null!;

	public PhoneNumber Phone { get; private set; } = null!;

	public FullName FullName { get; private set; } = null!;

	public DateOfBirth DateOfBirth { get; private set; } = null!;

	public string? Country { get; private set; }

	public ICollection<WatchList> WatchLists { get; private set; } = new List<WatchList>();

	private User() { }

	private User(Username username, HashedPassword hashedPassword, EmailAddress email, PhoneNumber phone, FullName fullName, DateOfBirth dob, string? country)
	{
		Id = Guid.CreateVersion7();
		Username = username;
		HashedPassword = hashedPassword;
		Email = email;
		Phone = phone;
		FullName = fullName;
		DateOfBirth = dob;
		Country = country;
	}

	public static User Create(Username username, HashedPassword hashedPassword, EmailAddress email, PhoneNumber phone, FullName fullName, DateOfBirth dob, string? country)
	{
		return new User(
			username,
			hashedPassword,
			email,
			phone,
			fullName,
			dob,
			country
		);
	}
}
