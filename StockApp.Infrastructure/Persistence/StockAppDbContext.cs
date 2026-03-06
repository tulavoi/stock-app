using StockApp.Domain.Entities.Notifications;
using StockApp.Domain.Entities.Orders;
using StockApp.Domain.Entities.Portfolios;
using StockApp.Domain.Entities.Quotes;
using StockApp.Domain.Entities.Stocks;
using StockApp.Domain.Entities.Transactions;
using StockApp.Domain.Entities.Users;
using StockApp.Domain.Entities.WatchLists;

namespace StockApp.Infrastructure.Persistence;

public class StockAppDbContext : DbContext
{
	public StockAppDbContext(DbContextOptions<StockAppDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Tự động tìm và áp dụng tất cả các class implement IEntityTypeConfiguration trong Assembly hiện tại.
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockAppDbContext).Assembly);
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Stock> Stocks { get; set; }
	public DbSet<WatchList> WatchLists { get; set; }
	public DbSet<RealTimeQuote> RealTimeQuotes { get; set; }
	public DbSet<Quote> Quotes { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Portfolio> Portfolios { get; set; }
	public DbSet<Notification> Notifications { get; set; }
	public DbSet<Transaction> Transactions { get; set; }
}
