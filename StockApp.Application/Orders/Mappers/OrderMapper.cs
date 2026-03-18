namespace StockApp.Application.Orders.Mappers;

public static class OrderMapper
{
	public static OrderDto ToOrderDto(this Order order)
	{
		return new OrderDto
		{
			Id = order.Id,
			UserId = order.UserId,
			StockId = order.StockId,
			Symbol = order.Stock?.Symbol ?? string.Empty,
			CompanyName = order.Stock?.CompanyName ?? string.Empty,
			Type = order.Type,
			Direction = order.Direction,
			Status = order.Status,
			RequestedPrice = order.RequestedPrice,
			Quantity = order.Quantity,
			OrderDate = order.OrderDate,
			ExecutedPrice = order.ExecutedPrice,
			ExecutedDate = order.ExecutedDate,
		};
	}
}
