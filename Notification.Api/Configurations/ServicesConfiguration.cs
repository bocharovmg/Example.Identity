using Infrastructure.Processors;
using Infrastructure.QueryHandlers;
using Infrastructure.Contracts.Commands;
using Notification.Api.BackgroundServices;

namespace Notification.Api.Configurations;

public static class ServicesConfiguration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddMediatR(
                configuration =>
                {
                    configuration.ConfigureCommandHandlers();

                    configuration.ConfigureQueryHandlers();
                }
            );

        services.AddHostedService<EmailOutboxBackgroundService>();

        services.AddTransient<IEmailOutboxProcessor, EmailOutboxProcessor>();

        return services;
    }

    private static void ConfigureCommandHandlers(this MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(typeof(NextOutboxMessageQueryHandler).Assembly);
    }

    private static void ConfigureQueryHandlers(this MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(typeof(RemoveOutboxMessageCommand).Assembly);
    }
}
