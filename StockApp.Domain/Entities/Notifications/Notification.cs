using StockApp.Domain.Entities.Users;

namespace StockApp.Domain.Entities.Notifications;

public class Notification
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public NotificationType Type { get; set; }
	public string Message { get; set; } = string.Empty;
	public bool IsRead { get; set; } = false;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public User User { get; set; } = null!;

	private Notification(Guid userId, NotificationType type, string content)
	{
		Id = Guid.CreateVersion7();
		UserId = userId;
		Type = type;
		Message = content;
		IsRead = false;
		CreatedAt = DateTime.UtcNow;
	}

	public static Notification Create(Guid userId,  NotificationType type, string content)
	{
		return new Notification(userId, type, content);
	}

	public void MarkAsRead() => IsRead = true;
}
