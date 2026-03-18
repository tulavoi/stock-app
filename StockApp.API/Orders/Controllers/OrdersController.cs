namespace StockApp.API.Orders.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrdersController : BaseController
{
	private readonly IOrderService _orderService;

	public OrdersController(IOrderService orderService)
	{
		_orderService = orderService;
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		var order = await _orderService.GetByIdAsync(id, cancellationToken);
		if (order == null) return NotFound();
		return Ok(order.ToOrderResponse());
	}

	[HttpPost]
	public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
	{
		if (request is null) return BadRequest();

		var userId = GetUserId();
		var dto = request.ToCreateOrderDto(userId);
		var result = await _orderService.CreateAsync(dto, cancellationToken);

		if (result.IsFailure) return ToProblemDetails(result);

		var orderResponse = result.Value.ToOrderResponse();
		return CreatedAtAction(nameof(GetById), new { id = orderResponse.Id }, orderResponse);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderRequest request, CancellationToken cancellationToken)
	{
		if (request is null) return BadRequest();

		var userId = GetUserId();
		var dto = request.ToUpdateOrderDto(userId, id);
		var result = await _orderService.UpdateAsync(dto, cancellationToken);

		if (result.IsFailure) return ToProblemDetails(result);

		var orderResponse = result.Value.ToOrderResponse();
		return Ok(orderResponse);
	}

	[HttpPost("{id:guid}/execute-test")]
	public async Task<IActionResult> ExecuteOrderTest(Guid id, CancellationToken cancellationToken)
	{
		var result = await _orderService.ExecuteAsync(id, cancellationToken);

		if (result.IsFailure)
			return ToProblemDetails(result);

		return Ok(result.Value);
	}

	[HttpGet]
	public async Task<IActionResult> GetUserOrders([FromQuery] OrderQuery query, CancellationToken cancellationToken)
	{
		var userId = GetUserId();

		var source = await _orderService.GetByUserIdAsync(query, userId, cancellationToken);
		
		var items = source.Items.Select(o => o.ToOrderResponse()).ToList();

		var response = PagedList<OrderResponse>.Create(items, source.PageNumber, source.PageSize, source.TotalCount);

		return Ok(response);
	}
}
