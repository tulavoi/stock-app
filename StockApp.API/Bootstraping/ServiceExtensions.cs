using System.Text.Json.Serialization;

namespace StockApp.API.Bootstraping;

public static class ServiceExtensions
{
	public static IServiceCollection AddRequiredServices(
		this IServiceCollection services, 
		IConfiguration config)
	{
		//services.AddOpenApi();

		// Swagger / OpenAPI
		//services.AddEndpointsApiExplorer();
		//services.AddSwaggerGen(options =>
		//{
		//	options.SwaggerDoc("v1", new OpenApiInfo
		//	{
		//		Title = "Stock App API",
		//		Version = "v1",
		//		Description = "API for Stock App"
		//	});

		//	// Cấu hình JWT Bearer cho Swagger
		//	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
		//	{
		//		Description = "Nhập token theo định dạng: Bearer {your token}",
		//		Name = "Authorization",
		//		In = ParameterLocation.Header,
		//		Type = SecuritySchemeType.ApiKey,
		//		Scheme = "Bearer",
		//		BearerFormat = "JWT",
		//	});

		//	options.AddSecurityRequirement(new OpenApiSecurityRequirement
		//	{
		//		{
		//			new OpenApiSecurityScheme
		//			{
		//				Reference = new OpenApiReference
		//				{
		//					Type = ReferenceType.SecurityScheme,
		//					Id = "Bearer"
		//				}
		//			},
		//			Array.Empty<string>()
		//		}
		//	});
		//});

		services.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

				// Enum nhận mọi dạng: "abc", "Abc", "ABC"
				options.JsonSerializerOptions.Converters.Add(
					new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
				);
			});

		// DI Services
		services.AddApplicationServices();
		services.AddInfrastructureServices(config);

		// JWT Authentication
		var jwtSettings = config.GetSection("Jwt");
		var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtSettings["Issuer"],
				ValidAudience = jwtSettings["Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ClockSkew = TimeSpan.Zero
			};
		});

		services.AddAuthorization();

		return services;
	}
}
