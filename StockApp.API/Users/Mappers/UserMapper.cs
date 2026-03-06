namespace StockApp.API.Users.Mappers;

public static class UserMapper
{
	public static RegisterDto ToRegisterDto(this RegisterRequest request)
	{
		return new RegisterDto
		{
			Username = request.Username,
			Email = request.Email,
			Password = request.Password,
			Phone = request.Phone,
			FullName = request.Fullname,
			DateOfBirth = request.DateOfBirth,
			Country = request.Country
		};
	}

	public static LoginDto ToLoginDto(this LoginRequest request)
	{
		return new LoginDto
		{
			Email = request.Email,
			Password = request.Password
		};
	}

	public static UserResponse ToUserResponse(this UserDto dto)
	{
		return new UserResponse
		{
			Id = dto.Id,
			Username = dto.Username,
			Email = dto.Email,
			Phone = dto.Phone,
			FullName = dto.FullName,
			DateOfBirth = dto.DateOfBirth,
			Country = dto.Country
		};
	}
}
