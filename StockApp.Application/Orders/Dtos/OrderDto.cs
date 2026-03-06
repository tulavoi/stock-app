namespace StockApp.Application.Orders.Dtos;

public class OrderDto
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid StockId { get; set; }

	public string Symbol { get; set; } = null!;
	public string CompanyName { get; set; } = null!;

	public OrderType Type { get; set; }
	public OrderDirection Direction { get; set; }
	public OrderStatus Status { get; set; }

	public int Quantity { get; set; }
	public decimal? Price { get; set; }
	public DateTime OrderDate { get; set; }
}
