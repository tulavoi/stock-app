namespace StockApp.API.Orders.Requests;

public class CreateOrderRequest
{
	public Guid StockId { get; set; }
	public OrderType Type { get; set; }
	public OrderDirection Direction { get; set; }
	public int Quantity { get; set; }
	public decimal? Price { get; set; } = 0;
}
