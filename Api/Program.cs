using Api.Configurations;
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureExceptionHandlers();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureDatabaseOptions(builder.Configuration);
builder.Services.RegisterServices();

#pragma warning disable ASP0000
using var serviceProvider = builder.Services.BuildServiceProvider();
#pragma warning restore ASP0000

var contextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

builder.Services.AddScoped(_ =>
{
    if (contextAccessor.HttpContext == null)
    {
        throw new ArgumentNullException($"{nameof(contextAccessor.HttpContext)} of type {typeof(HttpContext)}");
    }

    return contextAccessor.HttpContext.BuildUserContext();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => {
    options.LowercaseUrls = true;

    options.LowercaseQueryStrings = false;
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(builder => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
