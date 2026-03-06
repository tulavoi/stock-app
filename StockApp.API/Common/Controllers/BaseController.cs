namespace StockApp.API.Common.Controllers;

public abstract class BaseController : ControllerBase
{
	protected Guid GetUserId()
	{
		return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
	}

	protected IActionResult ToProblemDetails(Result result)
	{
		if (result.IsSuccess) throw new InvalidOperationException();

		var type = result.Errors.First().Type;

		return Problem(
			statusCode: GetStatusCode(type),
			title: GetTitle(type),
			extensions: new Dictionary<string, object?>
			{
				{ "errors", result.Errors }
			});
	}

	private int GetStatusCode(ErrorType type) => type switch
	{
		ErrorType.NotFound => StatusCodes.Status404NotFound,
		ErrorType.Validation => StatusCodes.Status400BadRequest,
		ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
		ErrorType.Forbidden => StatusCodes.Status403Forbidden,
		ErrorType.Conflict => StatusCodes.Status409Conflict,
		_ => StatusCodes.Status500InternalServerError
	};

	private string GetTitle(ErrorType type) => type switch
	{
		ErrorType.Validation => "Bad request",
		ErrorType.NotFound => "Not found",
		ErrorType.Conflict => "Conflict",
		ErrorType.Unauthorized => "Unauthorized",
		ErrorType.Forbidden => "Forbidden",
		_ => "An unexpected error occurred."
	};
}
