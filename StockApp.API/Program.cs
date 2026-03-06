var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRequiredServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
app.ConfigureRequestPipeline();

app.Run();