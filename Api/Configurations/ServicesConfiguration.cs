﻿using System.Reflection;
using Exemple.Identity.Domain.Configurations;
using Exemple.Identity.Infrastructure.Configuration;


namespace Exemple.Identity.Api.Configurations;

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
