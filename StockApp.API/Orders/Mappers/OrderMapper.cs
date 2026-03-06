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
			Price = request.Price,
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
			Price = dto.Price,
			OrderDate = dto.OrderDate
		};
	}
}
