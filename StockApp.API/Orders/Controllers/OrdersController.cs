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
	public async Task<IActionResult> GetById(Guid id)
	{
		var order = await _orderService.GetByIdAsync(id);
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
}
