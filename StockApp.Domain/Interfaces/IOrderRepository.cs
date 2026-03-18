namespace StockApp.Domain.Interfaces;

public interface IOrderRepository
{
	Task AddAsync(Order order, CancellationToken cancellationToken);
	Task UpdateAsync(Order order, CancellationToken cancellationToken);
	Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<(IEnumerable<Order> items, long totalCount)> GetByUserIdAsync(
		Guid userId,
		Guid? stockId,
		OrderType? type,
		OrderDirection? direction,
		IEnumerable<OrderStatus>? statuses,
		DateTime? fromDate,
		DateTime? toDate,
		string? sortBy,
		bool isDescending,
		int pageNumber,
		int pageSize,
		CancellationToken cancellationToken
	);
}
