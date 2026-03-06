using StockApp.Domain.Entities.Notifications;

namespace StockApp.Domain.Interfaces;

public interface INotificationRepository
{
	Task AddAsync(Notification notification);
}
