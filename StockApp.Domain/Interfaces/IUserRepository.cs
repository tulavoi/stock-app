using StockApp.Domain.Entities.Users;

namespace StockApp.Domain.Interfaces;

public interface IUserRepository
{
	Task<(IEnumerable<User> items, long totalCount)> GetAllAsync(
		bool isDecsending,
		string sortBy,
		int pageNumber,
		int pageSize,
		string? keyword,
		CancellationToken cancellationToken);

	Task<User> RegisterAsync(User user, CancellationToken cancellationToken);

	Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

	Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail);

	Task<User?> GetByUsernameAsync(Username username);

	Task<User?> GetByEmailAsync(EmailAddress email);

	Task<bool> ExistsByUsername(Username username, Guid? excludeId = null);

	Task<bool> ExistsByEmail(EmailAddress email, Guid? excludeId = null);
}
