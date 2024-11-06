using OutboxHandler.BackgroundServices;
using OutboxHandler.Interfaces;
using OutboxHandler.Processors.Email;
using OutboxHandler.Services.Outbox;


namespace OutboxHandler.Configurations;

internal static class ServicesConfiguration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddHostedService<OutboxBackgroundService<IEmailOutboxProcessor>>();

        services.AddTransient<IOutboxService, OutboxService>();

        services.AddTransient<IEmailOutboxProcessor, EmailOutboxProcessor>();

        return services;
    }
}
