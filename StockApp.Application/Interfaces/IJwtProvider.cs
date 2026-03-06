using StockApp.Domain.Entities.Users;

namespace StockApp.Application.Interfaces;

public interface IJwtProvider
{
	string GenerateToken(User user);
}
