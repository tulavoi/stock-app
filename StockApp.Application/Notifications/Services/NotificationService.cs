using StockApp.Domain.Entities.Notifications;
using StockApp.Domain.Entities.Orders;

namespace StockApp.Application.Notifications.Services;

public class NotificationService : INotificationService
{
	private readonly INotificationRepository _notificationRepo;
	private readonly IStockService _stockService;

	public NotificationService(INotificationRepository notificationRepo, IStockService stockService)
	{
		_notificationRepo = notificationRepo;
		_stockService = stockService;
	}

	public async Task CreateOrderExecutedAsync(Order order, CancellationToken cancellationToken)
	{
		var symbol = order.Stock.Symbol;

		var message = this.BuildMessage(order.Direction, order.Quantity, symbol);

		var notification = Notification.Create(order.UserId, NotificationType.OrderExecuted, message);

		await _notificationRepo.AddAsync(notification);
	}

	private string BuildMessage(OrderDirection direction, int quantity, string symbol)
	{
		var action = direction == OrderDirection.Buy ? "bought" : "sold";
		return $"You have successfully {action} {quantity} shares of {symbol}";
	}
}
