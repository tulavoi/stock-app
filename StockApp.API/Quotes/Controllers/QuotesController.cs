namespace StockApp.API.Quotes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuotesController : BaseController
{
	private readonly IQuoteService _quotesService;
	private readonly ILogger _logger;

	public QuotesController(IQuoteService quotesService, ILogger logger)
	{
		_quotesService = quotesService;
		_logger = logger;
	}

	[HttpGet("ws")]
	public async Task GetRealtimeQuotes([FromQuery] RealTimeQuoteQuery query, CancellationToken cancellationToken)
	{
		if (HttpContext.WebSockets.IsWebSocketRequest)
		{
			using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

			// Chuyển đổi danh sách quotes thành JSON, dạng camelCase
			var jsonOptions = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

			while (webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
			{
				try
				{
					// Gọi Service
					var quotes = await _quotesService.GetRealTimeQuoteAsync(query, cancellationToken);

					// Serialize 
					var json = JsonSerializer.Serialize(quotes, jsonOptions);
					var buffer = Encoding.UTF8.GetBytes(json);

					// Gửi data
					await webSocket.SendAsync(
						new ArraySegment<byte>(buffer),
						WebSocketMessageType.Text,
						true,
						cancellationToken
					);

				}
				catch (WebSocketException)
				{
					// Nếu lỗi do Client ngắt kết nối, thoát vòng lặp
					break;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error fetching real time quotes via WebSocket");
				}

				try
				{
					await Task.Delay(2000, cancellationToken); // Đợi 2 giây trước khi gửi giá trị tiếp theo
				}
				catch (TaskCanceledException)
				{
					break;
				}
			}
			await webSocket.CloseAsync(
				WebSocketCloseStatus.NormalClosure,
				"Connection closed by server",
				CancellationToken.None
			);
		}
		else
		{
			HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
		}
	}

	[HttpGet("historical")]
	public async Task<IActionResult> GetHistoricalQuotes([FromQuery] HistoricalQuoteQuery query, CancellationToken cancellationToken)
	{
		var result = await _quotesService.GetHistoricalQuotesAsync(query, cancellationToken);

		if (result.IsFailure) return ToProblemDetails(result);

		var response = result.Value.Select(q => q.ToHistoricalQuoteResponse());

		return Ok(response);
	}
}
