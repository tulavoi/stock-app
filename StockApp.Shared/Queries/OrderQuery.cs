namespace StockApp.Shared.Queries;

public class OrderQuery : BaseQuery
{
	public Guid? StockId { get; set; }

	public OrderType? Type { get; set; }
	public OrderDirection? Direction { get; set; }
	public List<OrderStatus>? Statuses { get; set; }

	public DateTime? FromDate { get; set; }
	public DateTime? ToDate { get; set; }
}
