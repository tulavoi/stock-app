using StockApp.Domain.Entities.Users;

namespace StockApp.Application.Users.Mappers;

public static class UserMapper
{
	public static UserDto ToUserDto(this User user)
	{
		return new UserDto
		{
			Id = user.Id,
			Username = user.Username.Value,
			Email = user.Email.Value,
			Country = user.Country,
			Phone = user.Phone.Value,
			FullName = user.FullName.Value,
			DateOfBirth = user.DateOfBirth.Value,
		};
	}

	public static LoginResponseDto ToLoginResponseDto(this User user, string token)
	{
		return new LoginResponseDto
		{
			User = user.ToUserDto(),
			Token = token
		};
	}
}
