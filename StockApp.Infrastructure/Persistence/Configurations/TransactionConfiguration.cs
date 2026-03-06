using StockApp.Domain.Entities.Transactions;

namespace StockApp.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
	public void Configure(EntityTypeBuilder<Transaction> builder)
	{
		builder.Property(n => n.Type).HasConversion<string>();
	}
}
