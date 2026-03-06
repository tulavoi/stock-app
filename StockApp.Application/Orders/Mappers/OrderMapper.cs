using StockApp.Domain.Entities.Orders;

namespace StockApp.Application.Orders.Mappers;

public static class OrderMapper
{
	public static Order ToOrder(this CreateOrderDto dto)
	{
		//return new Order(
		//	userId: dto.UserId,
		//	stockId: dto.StockId,
		//	type: dto.Type,
		//	direction: dto.Direction,
		//	quantity: dto.Quantity,
		//	price: dto.Price
		//);

		return dto.Type switch
		{
			OrderType.Market => Order.CreateMarketOrder(dto.UserId, dto.StockId, dto.Direction, dto.Quantity),
			OrderType.Limit => Order.CreateLimitOrder(dto.UserId, dto.StockId, dto.Direction, dto.Quantity, dto.Price!.Value),
			OrderType.Stop => Order.CreateStopOrder(dto.UserId, dto.StockId, dto.Direction, dto.Quantity, dto.Price!.Value),
			_ => throw new ArgumentOutOfRangeException()
		};
	}

	public static OrderDto ToOrderDto(this Order order)
	{
		return new OrderDto
		{
			Id = order.Id,
			UserId = order.UserId,
			StockId = order.StockId,
			Symbol = order.Stock.Symbol,
			CompanyName = order.Stock.CompanyName,
			Type = order.Type,
			Direction = order.Direction,
			Status = order.Status,
			Price = order.Price,
			Quantity = order.Quantity,
			OrderDate = order.OrderDate
		};
	}
}
