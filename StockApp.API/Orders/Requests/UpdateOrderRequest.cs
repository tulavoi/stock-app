namespace StockApp.API.Orders.Requests;

public class UpdateOrderRequest
{
	public int Quantity	{ get; set; }
	public decimal? RequestedPrice { get; set; }
}
