using StockApp.Application.Portfolios.Mappers;

namespace StockApp.Application.Portfolios.Services;

public class PortfolioService : IPortfolioService
{
	private readonly IPortfolioRepository _portfolioRepo;

	public PortfolioService(IPortfolioRepository portfolioRepo)
	{
		_portfolioRepo = portfolioRepo;
	}

	public async Task<Result> ApplyOrderAsync(
		Guid userId, Guid stockId, OrderDirection direction, 
		int quantity, decimal price, CancellationToken cancellationToken)
	{
		var portfolio = await _portfolioRepo.GetByUserAndStockAsync(userId, stockId, cancellationToken);

		// Case 1: Chưa có Portfolio
		if (portfolio is null)
		{
			if (direction == OrderDirection.Sell) return Result.Failure(PortfolioErrors.NotFound);

			var createResult = Portfolio.Create(userId, stockId, quantity, price);

			if (createResult.IsFailure) return Result.Failure(createResult.Errors);

			await _portfolioRepo.AddAsync(createResult.Value, cancellationToken);

			return Result.Success();
		}

		// Case 2: Đã có Portfolio -> Cập nhật
		var result = portfolio.ApplyOrder(direction, quantity, price);

		if (result.IsFailure) return result;

		await _portfolioRepo.UpdateAsync(portfolio, cancellationToken);
		return Result.Success();
	}

	public async Task<int> GetAvailableStockQuantityAsync(Guid userId, Guid stockId, CancellationToken cancellationToken)
	{
		return await _portfolioRepo.GetTotalQuantityAsync(userId, stockId, cancellationToken);
	}

	public async Task<IEnumerable<PortfolioDto>> GetUserPortfolioAsync(Guid userId, CancellationToken cancellationToken)
	{
		var portfolios = await _portfolioRepo.GetByUserIdAsync(userId, cancellationToken);

		return portfolios.Select(p => p.ToPortfolioDto());
	}
}
