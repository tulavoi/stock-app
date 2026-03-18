namespace StockApp.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly StockAppDbContext _db;

	public OrderRepository(StockAppDbContext db)
	{
		_db = db;
	}

	public async Task AddAsync(Order order, CancellationToken cancellationToken)
	{
		_db.Orders.Add(order);
		await _db.SaveChangesAsync(cancellationToken);
	}

	public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await _db.Orders
			.Include(o => o.User)
			.Include(o => o.Stock)
			.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
	}

	public async Task<(IEnumerable<Order> items, long totalCount)> GetByUserIdAsync(
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
		CancellationToken cancellationToken)
	{
		var query = _db.Orders
			.AsNoTracking()
			.Where(o => o.UserId == userId);

		if (stockId.HasValue) 
			query = query.Where(o => o.StockId == stockId.Value);

		if (type.HasValue) 
			query = query.Where(o => o.Type == type.Value);

		if (direction.HasValue) 
			query = query.Where(o => o.Direction == direction.Value);

		if (statuses != null && statuses.Any())
			query = query.Where(o => statuses.Contains(o.Status));

		if (fromDate.HasValue)
			query = query.Where(o => o.OrderDate >= fromDate.Value);

		if (toDate.HasValue) 
			query = query.Where(o => o.OrderDate <= toDate.Value);

		var totalCount = await query.LongCountAsync(cancellationToken);

		IQueryable<Order> finalQuery = query
			.Include(o => o.Stock);

		finalQuery = sortBy?.ToLower() switch
		{
			"orderdate" => isDescending ? finalQuery.OrderByDescending(o => o.OrderDate) : finalQuery.OrderBy(o => o.OrderDate),
			"executeddate" => isDescending ? finalQuery.OrderByDescending(o => o.ExecutedDate) : finalQuery.OrderBy(o => o.ExecutedDate),
			"price" => isDescending ? finalQuery.OrderByDescending(o => o.RequestedPrice) : finalQuery.OrderBy(o => o.RequestedPrice),
			_ => finalQuery.OrderByDescending(o => o.OrderDate)
		};

		var orders = await finalQuery
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);

		return (orders, totalCount);
	}

	public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
	{
		_db.Orders.Update(order);
		await _db.SaveChangesAsync(cancellationToken);
	}
}
