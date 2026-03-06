namespace StockApp.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
	{
		services.AddDbContext<StockAppDbContext>(options =>
			options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IWatchListRepository, WatchListRepository>();
		services.AddScoped<IStockRepository, StockRepository>();
		services.AddScoped<IQuoteRepository, QuoteRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<IPortfolioRepository, PortfolioRepository>();
		services.AddScoped<INotificationRepository, NotificationRepository>();
		services.AddScoped<ITransactionRepository, TransactionRepository>();

		services.AddSingleton<IPasswordHasher, PasswordHasher>();
		services.AddSingleton<IJwtProvider, JwtProvider>();
		services.AddScoped<IUnitOfWork, EfUnitOfWork>();

		return services;
	}
}
