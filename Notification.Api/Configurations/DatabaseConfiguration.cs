using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.ConnectionManager;


namespace Notification.Api.Configurations;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabaseOptions(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile("database-config.json");

        services.Configure<SqlConnectionOptions>(configurationManager);

        services.AddScoped<ISqlConnectionManager, SqlConnectionManager>();

        return services;
    }
}
