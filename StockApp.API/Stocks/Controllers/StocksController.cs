using StockApp.Application.Commons;

namespace StockApp.API.Stocks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StocksController : BaseController
{
	private readonly IStockService _stockService;

	public StocksController(IStockService stockService)
	{
		_stockService = stockService;
	}
	
	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] StockQuery query, CancellationToken cancellationToken)
	{
		var source = await _stockService.GetAllAsync(query, cancellationToken);

		var items = source.Items.Select(s => s.ToStockResponse()).ToList();

		var response = PagedList<StockResponse>.Create(items, source.PageNumber, source.PageSize, source.TotalCount);

		return Ok(response);
	}
	
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		var result = await _stockService.GetByIdAsync(id, cancellationToken);

		if (result.IsFailure) return ToProblemDetails(result);

		return Ok(result.Value.ToStockResponse());
	}
}
