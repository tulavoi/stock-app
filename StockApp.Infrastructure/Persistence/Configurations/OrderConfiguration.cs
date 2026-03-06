using StockApp.Domain.Entities.Orders;

namespace StockApp.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.Property(o => o.Type).HasConversion<string>();
		builder.Property(o => o.Direction).HasConversion<string>();
		builder.Property(o => o.Status).HasConversion<string>();
	}
}
