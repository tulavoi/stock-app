
using StockApp.Domain.Entities.Orders;

namespace StockApp.Domain.Interfaces;

public interface IOrderRepository
{
	Task AddAsync(Order order);
	Task UpdateAsync(Order order);
	Task<Order?> GetByIdAsync(Guid id);
}
