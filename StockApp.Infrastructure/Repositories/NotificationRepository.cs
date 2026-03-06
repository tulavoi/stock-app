using StockApp.Domain.Entities.Notifications;

namespace StockApp.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
	private readonly StockAppDbContext _db;

	public NotificationRepository(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task AddAsync(Notification notification)
	{
		_db.Notifications.Add(notification);
		await _db.SaveChangesAsync();
	}
}
