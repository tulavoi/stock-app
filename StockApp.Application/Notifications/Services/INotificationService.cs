using StockApp.Domain.Entities.Orders;

namespace StockApp.Application.Notifications.Services;

public interface INotificationService
{
	Task CreateOrderExecutedAsync(Order order, CancellationToken cancellationToken);
}
