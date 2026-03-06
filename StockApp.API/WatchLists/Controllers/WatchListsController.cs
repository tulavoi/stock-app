namespace StockApp.API.WatchLists.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WatchListsController : BaseController
{
	private readonly IWatchListService _watchListService;

	public WatchListsController(IWatchListService watchListService)
	{
		_watchListService = watchListService;
	}

	[HttpPost("add")]
	public async Task<IActionResult> AddToWatchList([FromBody] AddToWatchListRequest request, CancellationToken cancellationToken)
	{
		if (request is null) return BadRequest();

		var userId = GetUserId();
		var dto = request.ToAddToWatchListDto(userId);
		var result = await _watchListService.AddToWatchListAsync(dto, cancellationToken);

		if (result.IsFailure) return ToProblemDetails(result);

		return Ok(new { Response = result.Value });
	}

	[HttpGet]
	public async Task<IActionResult> GetAllWatchLists()
	{
		var userId = GetUserId();
		var watchList = await _watchListService.GetAllAsync(userId);
		return Ok(watchList.Select(wl => wl.ToWatchListResponse()));
	}
}
