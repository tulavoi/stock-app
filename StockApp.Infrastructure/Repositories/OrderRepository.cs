using StockApp.Domain.Entities.Orders;

namespace StockApp.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly StockAppDbContext _db;

	public OrderRepository(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task AddAsync(Order order)
	{
		_db.Orders.Add(order);
		await _db.SaveChangesAsync();
	}

	public async Task<Order?> GetByIdAsync(Guid id)
	{
		return await _db.Orders
			.Include(o => o.User)
			.Include(o => o.Stock)
			.FirstOrDefaultAsync(o => o.Id == id);
	}

	public async Task UpdateAsync(Order order)
	{
		_db.Orders.Update(order);
		await _db.SaveChangesAsync();
	}
}
