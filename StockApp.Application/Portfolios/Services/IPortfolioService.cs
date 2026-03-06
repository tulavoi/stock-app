namespace StockApp.Application.Portfolios.Services;

public interface IPortfolioService
{
	Task<Result> ApplyOrderAsync(Guid userId,
		Guid stockId,
		OrderDirection direction,
		int quantity,
		decimal price
	);
}
