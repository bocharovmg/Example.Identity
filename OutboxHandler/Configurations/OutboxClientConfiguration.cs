using OutboxHandler.Services.Outbox.Settings;

namespace OutboxHandler.Configurations;

internal static class OutboxClientConfiguration
{
    public static IServiceCollection ConfigureOutboxClientOptions(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile("outbox-client-config.json");

        services.Configure<ApiSettings>(configurationManager);

        return services;
    }
}
