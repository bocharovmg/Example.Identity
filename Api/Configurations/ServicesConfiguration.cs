using System.Reflection;
using Domain.Configurations;
using Infrastructure.Configuration;


namespace Api.Configurations;

public static class ServicesConfiguration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddMemoryCache();

        services.ConfigureDomainServices();

        services.ConfigureInfrastructureServices();

        services
            .AddMediatR(
                configuration =>
                {
                    configuration.ConfigureDomainBehaviors();

                    configuration.ConfigureDomainHandlers();

                    configuration.ConfigureInfrastructureHandlers();
                }
            );

        services
            .AddAutoMapper(
                (configuration) =>
                {
                    configuration.ConfigureDomainMappers();

                    configuration.ConfigureInfrastructureMappers();
                },
                Assembly.GetExecutingAssembly()
            );

        return services;
    }
}
