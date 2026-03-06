using StockApp.Domain.Entities.WatchLists;

namespace StockApp.Infrastructure.Persistence.Configurations;

public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
{
	public void Configure(EntityTypeBuilder<WatchList> builder)
	{
		builder.HasKey(wl => new { wl.UserId, wl.StockId });

		builder.HasOne(w => w.User)
			.WithMany(u => u.WatchLists)
			.HasForeignKey(w => w.UserId);

		builder.HasOne(w => w.Stock)
			.WithMany(s => s.WatchLists)
			.HasForeignKey(w => w.StockId);
	}
}
