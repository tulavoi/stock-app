using StockApp.Domain.Entities.Stocks;
using StockApp.Domain.Entities.Users;

namespace StockApp.Domain.Entities.Portfolios;

public class Portfolio
{
	public Guid UserId { get; set; }
	public Guid StockId { get; set; }
	public int Quantity { get; set; }
	public decimal AvaragePrice { get; set; }
	public DateTime UpdatedAt { get; set; }

	public User User { get; set; } = null!;
	public Stock Stock { get; set; } = null!;

	public Portfolio() { }

	private Portfolio(Guid userId, Guid stockId, int quantity, decimal avaragePrice)
	{
		UserId = userId;
		StockId = stockId;
		Quantity = quantity;
		AvaragePrice = avaragePrice;
		UpdatedAt = DateTime.UtcNow;
	}

	public static Result<Portfolio> Create(Guid userId, Guid stockId, int quantity, decimal price)
	{
		if (quantity <= 0)
			return Result<Portfolio>.Failure(PortfolioErrors.InvalidQuantity);

		// Giá có thể = 0 (quà tặng) nhưng không được âm
		if (price < 0)
			return Result<Portfolio>.Failure(PortfolioErrors.InvalidPrice);

		var portfolio = new Portfolio(userId, stockId, quantity, price);
		return Result<Portfolio>.Success(portfolio);
	}

	public Result ApplyOrder(OrderDirection direction, int quantity, decimal price)
	{
		if (quantity <= 0)
			return Result.Failure(PortfolioErrors.InvalidQuantity);

		if (price <= 0m)
			return Result.Failure(PortfolioErrors.InvalidPrice);

		if (direction == OrderDirection.Buy)
			return AddShares(quantity, price);
		else
			return RemoveShares(quantity);
	}

	private Result AddShares(int quantity, decimal price)
	{
		// Logic tính giá bình quân gia quyền (Weighted Average Price)
		var totalCost = (this.Quantity * this.AvaragePrice) + (quantity * price);
		var newQuantity = this.Quantity + quantity;

		// Cập nhật giá trung bình mới
		this.AvaragePrice = totalCost / newQuantity;

		// Cập nhật số lượng 
		this.Quantity = newQuantity;
		this.UpdatedAt = DateTime.UtcNow;

		return Result.Success();
	}

	private Result RemoveShares(int quantity)
	{
		if (quantity > Quantity)
			return Result.Failure(PortfolioErrors.InsufficientQuantity(this.StockId));

		this.Quantity -= quantity;
		this.UpdatedAt = DateTime.UtcNow;

		return Result.Success();
	}
}
