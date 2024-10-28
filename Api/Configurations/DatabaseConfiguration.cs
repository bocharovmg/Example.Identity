using Exemple.Identity.Infrastructure.ConnectionManager;


namespace Exemple.Identity.Api.Configurations;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabaseOptions(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile("database-config.json");

        services.Configure<SqlConnectionOptions>(configurationManager);

        return services;
    }
}
