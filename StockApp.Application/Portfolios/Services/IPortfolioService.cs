namespace StockApp.Application.Portfolios.Services;

public interface IPortfolioService
{
	Task<Result> ApplyOrderAsync(Guid userId,
		Guid stockId,
		OrderDirection direction,
		int quantity,
		decimal price,
		CancellationToken cancellationToken
	);
	Task<int> GetAvailableStockQuantityAsync(Guid userId, Guid stockId, CancellationToken cancellationToken);
	Task<IEnumerable<PortfolioDto>> GetUserPortfolioAsync(Guid userId, CancellationToken cancellationToken);
}
