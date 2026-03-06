namespace StockApp.Application.Users.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepo;
	private readonly IPasswordHasher _passwordHasher;
	private readonly IJwtProvider _jwtProvider;

	public UserService(IUserRepository userRepo, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
	{
		_userRepo = userRepo;
		_passwordHasher = passwordHasher;
		_jwtProvider = jwtProvider;
	}

	public Task<bool> DeleteAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<Result<PagedList<UserDto>>> GetAllAsync(UserQuery query, CancellationToken cancellationToken)
	{
		var (users, totalCount) = await _userRepo.GetAllAsync(
			query.IsDecsending, 
			query.SortBy, 
			query.PageNumber, 
			query.PageSize, 
			query.Keyword, 
			cancellationToken
		);

		var dtos = users.Select(u => u.ToUserDto()).ToList();

		var pagedList = PagedList<UserDto>.Create(dtos, query.PageNumber, query.PageSize, totalCount);

		return Result<PagedList<UserDto>>.Success(pagedList);
	}

	public async Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var user = await _userRepo.GetByIdAsync(id, cancellationToken);

		if (user is null)
			return Result<UserDto>.Failure(UserErrors.NotFound(id));

		return Result<UserDto>.Success(user.ToUserDto());
	}

	public async Task<Result<LoginResponseDto>> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
	{
		var user = await _userRepo.GetByUsernameOrEmailAsync(dto.Email);
		
		if (user == null || !_passwordHasher.Verify(user.HashedPassword.Value, dto.Password))
			return Result<LoginResponseDto>.Failure(AuthenticationErrors.InvalidCredentials);

		var token = _jwtProvider.GenerateToken(user);
		var responseDto = user.ToLoginResponseDto(token);
		return Result<LoginResponseDto>.Success(responseDto);
	}

	public async Task<Result<UserDto>> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken)
	{
		if (dto == null)
			return Result<UserDto>.Failure(Error.NullValue);

		var usernameResult = Username.Create(dto.Username);
		var emailResult = EmailAddress.Create(dto.Email);
		var phoneResult = PhoneNumber.Create(dto.Phone);
		var fullNameResult = FullName.Create(dto.FullName);
		var dobResult = DateOfBirth.Create(dto.DateOfBirth);
		var passwordResult = Password.Create(dto.Password);

		var results = new Result[] { 			
			usernameResult,
			emailResult,
			phoneResult,
			fullNameResult,
			dobResult,
			passwordResult
		};

		var errors = results
			.Where(r => r.IsFailure)
			.SelectMany(r => r.Errors)
			.ToList();

		if (errors.Any())
			return Result<UserDto>.Failure(errors);

		var conflictErrors = new List<Error>();

		var uniqueResult = await CheckUniqueAsync(usernameResult.Value, emailResult.Value);
		
		if (uniqueResult.IsFailure)
			return Result<UserDto>.Failure(uniqueResult.Errors);

		var hashedPassword = _passwordHasher.Hash(passwordResult.Value.Value);
		var hashedPasswordResult = HashedPassword.Create(hashedPassword);

		if (hashedPasswordResult.IsFailure)
			return Result<UserDto>.Failure(hashedPasswordResult.Errors);

		var user = User.Create(
			usernameResult.Value,
			hashedPasswordResult.Value,
			emailResult.Value,
			phoneResult.Value,
			fullNameResult.Value,
			dobResult.Value,
			dto.Country
		);

		var registeredUser = await _userRepo.RegisterAsync(user, cancellationToken);
		return Result<UserDto>.Success(registeredUser.ToUserDto());
	}

	private async Task<Result> CheckUniqueAsync(Username username, EmailAddress email)
	{
		var errors = new List<Error>();

		if (await _userRepo.ExistsByUsername(username))
			errors.Add(UserErrors.UsernameNotUnique(username.Value));

		if (await _userRepo.ExistsByEmail(email))
			errors.Add(UserErrors.EmailNotUnique(email.Value));

		if (errors.Count > 0)
			return Result.Failure(errors);

		return Result.Success();
	}

	public Task<Result<UserDto>> UpdateProfileAsync(UserDto dto)
	{
		throw new NotImplementedException();
	}
}
