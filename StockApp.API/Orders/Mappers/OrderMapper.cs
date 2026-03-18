namespace StockApp.API.Orders.Mappers;

public static class OrderMapper
{
	public static CreateOrderDto ToCreateOrderDto(this CreateOrderRequest request, Guid userId)
	{
		return new CreateOrderDto
		{
			UserId = userId,
			StockId = request.StockId,
			Type = request.Type,
			Direction = request.Direction,
			Quantity = request.Quantity,
			RequestedPrice = request.RequestedPrice,
		};
	}

	public static UpdateOrderDto ToUpdateOrderDto(this UpdateOrderRequest request, Guid userId, Guid orderId)
	{
		return new UpdateOrderDto
		{
			UserId = userId,
			OrderId = orderId,
			Quantity = request.Quantity,
			RequestedPrice = request.RequestedPrice,
		};
	}

	public static OrderResponse ToOrderResponse(this OrderDto dto)
	{
		return new OrderResponse
		{
			Id = dto.Id,
			StockId = dto.StockId,
			Symbol = dto.Symbol,
			CompanyName = dto.CompanyName,
			Type = dto.Type,
			Direction = dto.Direction,
			Status = dto.Status,
			Quantity = dto.Quantity,
			RequestedPrice = dto.RequestedPrice,
			OrderDate = dto.OrderDate,
			ExecutedPrice = dto.ExecutedPrice,
			ExecutedDate = dto.ExecutedDate
		};
	}
}
