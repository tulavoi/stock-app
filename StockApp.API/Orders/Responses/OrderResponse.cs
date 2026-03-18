namespace StockApp.API.Orders.Responses;

public class OrderResponse
{
	public Guid Id { get; set; }
	public Guid StockId { get; set; }
	public string Symbol { get; set; } = null!;
	public string CompanyName { get; set; } = null!;

	public OrderType Type { get; set; }
	public OrderDirection Direction { get; set; }
	public OrderStatus Status { get; set; }

	public int Quantity { get; set; }
	public decimal? RequestedPrice { get; set; }
	public DateTime OrderDate { get; set; }

	public decimal? ExecutedPrice { get; set; }
	public DateTime? ExecutedDate { get; set; }
}
