using StockApp.Domain.Entities.Quotes;

namespace StockApp.Infrastructure.Persistence.Configurations;

public class RealTimeQuoteConfiguration : IEntityTypeConfiguration<RealTimeQuote>
{
	public void Configure(EntityTypeBuilder<RealTimeQuote> builder)
	{
		builder.HasNoKey().ToView("v_RealTime_Quotes");
	}
}
