using StockApp.Domain.Entities.Notifications;

namespace StockApp.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
	public void Configure(EntityTypeBuilder<Notification> builder)
	{
		builder.Property(n => n.Type).HasConversion<string>();
	}
}
