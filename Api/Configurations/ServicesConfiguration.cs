using System.Reflection;
using Domain.Configurations;
using Infrastructure.Configuration;
using FluentValidation;


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

        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
