using StockApp.Application.Commons;

namespace StockApp.API.Users.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
	private readonly IUserService _userService;

	public UsersController(IUserService userService)
	{
		_userService = userService;
	}

	[Authorize]
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		var result = await _userService.GetByIdAsync(id, cancellationToken);
		if (result.IsFailure) return ToProblemDetails(result);
		return Ok(result.Value.ToUserResponse());
	}

	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] UserQuery query, CancellationToken cancellationToken)
	{
		var result = await _userService.GetAllAsync(query, cancellationToken);

		var source = result.Value;
		var items = source.Items.Select(user => user.ToUserResponse()).ToList();

		var response = PagedList<UserResponse>.Create(
			items,
			source.PageNumber,
			source.PageSize,
			source.TotalCount		
		);

		return Ok(response);
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
	{
		var dto = request.ToRegisterDto();
		var result = await _userService.RegisterAsync(dto, cancellationToken);

		if (result.IsFailure) return ToProblemDetails(result);

		var response = result.Value.ToUserResponse();
		return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
	{
		if (request is null) return BadRequest();

		var dto = request.ToLoginDto();
		var result = await _userService.LoginAsync(dto, cancellationToken);

		if (result.IsFailure) return ToProblemDetails(result);

		return Ok(new { Response = result.Value! });
	}
}
