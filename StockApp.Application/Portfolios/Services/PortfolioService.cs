using StockApp.Domain.Entities.Portfolios;

namespace StockApp.Application.Portfolios.Services;

public class PortfolioService : IPortfolioService
{
	private readonly IPortfolioRepository _portfolioRepo;

	public PortfolioService(IPortfolioRepository portfolioRepo)
	{
		_portfolioRepo = portfolioRepo;
	}

	public async Task<Result> ApplyOrderAsync(Guid userId, Guid stockId, OrderDirection direction, int quantity, decimal price)
	{
		var portfolio = await _portfolioRepo.GetByUserAndStockAsync(userId, stockId);

		// Case 1: Chưa có Portfolio
		if (portfolio is null)
		{
			if (direction == OrderDirection.Sell) return Result.Failure(PortfolioErrors.NotFound);

			var createResult = Portfolio.Create(userId, stockId, quantity, price);

			if (createResult.IsFailure) return Result.Failure(createResult.Errors);

			await _portfolioRepo.AddAsync(createResult.Value);

			return Result.Success();
		}

		// Case 2: Đã có Portfolio -> Cập nhật
		var result = portfolio.ApplyOrder(direction, quantity, price);

		if (result.IsFailure) return result;

		await _portfolioRepo.UpdateAsync(portfolio);
		return Result.Success();
	}
}
