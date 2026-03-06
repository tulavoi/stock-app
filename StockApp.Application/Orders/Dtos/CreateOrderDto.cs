namespace StockApp.Application.Orders.Dtos;

public class CreateOrderDto
{
	public Guid UserId { get; set; }
	public Guid StockId { get; set; }
	public OrderType Type { get; set; }
	public OrderDirection Direction { get; set; }
	public int Quantity { get; set; }
	public decimal? Price { get; set; }
}
