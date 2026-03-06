using StockApp.Domain.Entities.Users;

namespace StockApp.Infrastructure.External;

public class JwtProvider : IJwtProvider
{
	private readonly IConfiguration _config;

	public JwtProvider(IConfiguration config)
	{
		_config = config;
	}

	public string GenerateToken(User user)
	{
		var secretKey = _config["Jwt:Key"]!;
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
		var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity([
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, user.Username.Value),
				new Claim(JwtRegisteredClaimNames.Email, user.Email.Value)
			]),
			Expires = DateTime.UtcNow.AddHours(_config.GetValue<int>("Jwt:ExpireHours")),
			SigningCredentials = creds,
			Issuer = _config["Jwt:Issuer"],
			Audience = _config["Jwt:Audience"]
		};

		var handler = new JsonWebTokenHandler();

		string token = handler.CreateToken(tokenDescriptor);
		return token;
	}
}
