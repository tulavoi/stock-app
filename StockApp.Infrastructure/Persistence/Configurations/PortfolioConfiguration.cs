using StockApp.Domain.Entities.Portfolios;

namespace StockApp.Infrastructure.Persistence.Configurations;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
	public void Configure(EntityTypeBuilder<Portfolio> builder)
	{
		builder.HasKey(p => new { p.UserId, p.StockId });
	}
}
