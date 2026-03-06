namespace StockApp.Application.Orders.Services;

public interface IOrderService
{
	Task<Result<OrderDto>> CreateAsync(CreateOrderDto dto, CancellationToken cancellationToken);
	Task<Result<OrderDto>> ExecuteAsync(Guid orderId, CancellationToken cancellationToken);
	Task<OrderDto?> GetByIdAsync(Guid id);
}
