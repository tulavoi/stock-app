using StockApp.Domain.Entities.Users;

namespace StockApp.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.OwnsOne(x => x.Username, 
			b => b.Property(v => v.Value).HasColumnName("Username").IsRequired()
		);

		builder.OwnsOne(x => x.HashedPassword,
			b => b.Property(v => v.Value).HasColumnName("HashedPassword").IsRequired()
		);

		builder.OwnsOne(x => x.Email,
			b => b.Property(v => v.Value).HasColumnName("Email").IsRequired()
		);

		builder.OwnsOne(x => x.Phone,
			b => b.Property(v => v.Value).HasColumnName("Phone").IsRequired()
		);

		builder.OwnsOne(x => x.FullName,
			b => b.Property(v => v.Value).HasColumnName("FullName").IsRequired()
		);

		builder.OwnsOne(x => x.DateOfBirth,
			b => b.Property(v => v.Value).HasColumnName("DateOfBirth").IsRequired()
		);
	}
}
