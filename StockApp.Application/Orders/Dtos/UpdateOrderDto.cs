namespace StockApp.Application.Orders.Dtos;

public class UpdateOrderDto
{
	public Guid UserId { get; set; }
	public Guid OrderId { get; set; }
	public int Quantity { get; set; }
	public decimal? RequestedPrice { get; set; }
}
