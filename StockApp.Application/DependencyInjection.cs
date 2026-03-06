namespace StockApp.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IWatchListService, WatchListService>();
		services.AddScoped<IStockService, StockService>();
		services.AddScoped<IQuoteService, QuoteService>();
		services.AddScoped<IOrderService, OrderService>();
		services.AddScoped<IPortfolioService, PortfolioService>();
		services.AddScoped<INotificationService, NotificationService>();
		services.AddScoped<ITransactionService, TransactionService>();

		return services;
	}
}
