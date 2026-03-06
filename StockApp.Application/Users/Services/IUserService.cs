using StockApp.Application.Users.Queries;

namespace StockApp.Application.Users.Services;

public interface IUserService
{
	Task<Result<PagedList<UserDto>>> GetAllAsync(UserQuery query, CancellationToken cancellationToken);
	Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<Result<UserDto>> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken);
	Task<Result<LoginResponseDto>> LoginAsync(LoginDto dto, CancellationToken cancellationToken);
	Task<Result<UserDto>> UpdateProfileAsync(UserDto dto);
	Task<bool> DeleteAsync(Guid id);
}
