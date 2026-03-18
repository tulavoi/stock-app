namespace StockApp.API.Portfolios;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PortfoliosController : BaseController
{
	private readonly IPortfolioService _portfolioService;

	public PortfoliosController(IPortfolioService portfolioService)
	{
		_portfolioService = portfolioService;
	}

	[HttpGet("my-portfolio")]
	public async Task<IActionResult> GetUserPortfolio(CancellationToken cancellationToken)
	{
		var userId = GetUserId();

		var portfolios = await _portfolioService.GetUserPortfolioAsync(userId, cancellationToken);

		return Ok(portfolios.Select(p => p.ToPortfolioResponse()));
	}
}
