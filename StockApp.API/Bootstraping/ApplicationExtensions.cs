namespace StockApp.API.Bootstraping;

public static class ApplicationExtensions
{
	public static WebApplication ConfigureRequestPipeline(this WebApplication app)
	{
		// Cấu hình cho môi trường phát triển (Development)
		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
			//app.UseSwagger();
			//app.UseSwaggerUI();
		}

		// Cấu hình Middleware
		app.UseHttpsRedirection();
		app.UseAuthentication();
		app.UseAuthorization();

		// Map Controllers
		app.MapControllers();

		//app.MapGet("/", () => Results.Redirect("/swagger"));

		var webSocketOptions = new WebSocketOptions
		{
			KeepAliveInterval = TimeSpan.FromMinutes(2)
		};
		app.UseWebSockets();

		return app;
	}
}
