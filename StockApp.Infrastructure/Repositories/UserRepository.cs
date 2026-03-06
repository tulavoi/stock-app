using StockApp.Domain.Entities.Users;

namespace StockApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
	private readonly StockAppDbContext _db;
	public UserRepository(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task<User> RegisterAsync(User user, CancellationToken cancellationToken)
	{
		_db.Users.Add(user);
		await _db.SaveChangesAsync();
		return user;
	}

	public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await _db.Users.FindAsync(id);
	}

	public async Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail)
	{
		return await _db.Users.FirstOrDefaultAsync(u => u.Username.Value == usernameOrEmail || u.Email.Value == usernameOrEmail);
	}

	public async Task<bool> ExistsByUsername(Username username, Guid? excludeId = null)
	{
		return await _db.Users.AnyAsync(u => u.Username.Value == username.Value && 
			(!excludeId.HasValue || u.Id != excludeId)
		); 
	}

	public async Task<bool> ExistsByEmail(EmailAddress email, Guid? excludeId = null)
	{
		return await _db.Users.AnyAsync(u => u.Email.Value == email.Value &&
			(!excludeId.HasValue || u.Id != excludeId)
		);
	}

	public async Task<(IEnumerable<User> items, long totalCount)> GetAllAsync(
		bool isDecsending,
		string sortBy,
		int pageNumber,
		int pageSize,
		string? keyword,
		CancellationToken cancellationToken)
	{
		var query = _db.Users.AsNoTracking().AsQueryable();
		
		// Searching
		if (!string.IsNullOrEmpty(keyword))
			query = query.Where(u => u.Username.Value.Contains(keyword) || u.Email.Value.Contains(keyword));

		// Sorting
		query = sortBy?.ToLower() switch
		{
			"username" => isDecsending ? query.OrderByDescending(u => u.Username.Value) : query.OrderBy(u => u.Username.Value),
			"email" => isDecsending ? query.OrderByDescending(u => u.Email.Value) : query.OrderBy(u => u.Email.Value),
			_ => query.OrderBy(u => u.Username.Value)
		};

		var totalCount = await query.LongCountAsync(cancellationToken);

		// Pagination
		var items = await query
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);

		return (items, totalCount);
	}

	public async Task<User?> GetByUsernameAsync(Username username)
	{
		return await _db.Users.FirstOrDefaultAsync(u => u.Username.Value == username.Value);
	}

	public async Task<User?> GetByEmailAsync(EmailAddress email)
	{
		return await _db.Users.FirstOrDefaultAsync(u => u.Email.Value == email.Value);
	}
}
