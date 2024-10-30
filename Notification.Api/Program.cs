using Notification.Api.Configurations;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureDatabaseOptions(builder.Configuration);
builder.Services.ConfigureEmailOptions(builder.Configuration);
builder.Services.RegisterServices();

var app = builder.Build();

app.Run();
