using OutboxHandler.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureOutboxClientOptions(builder.Configuration);
builder.Services.ConfigureEmailOptions(builder.Configuration);
builder.Services.RegisterServices();

var app = builder.Build();

app.Run();
