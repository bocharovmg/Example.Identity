using AutoMapper;
using Domain.Behaviors;
using Domain.CommandHandlers;
using Domain.Configurations.Profiles;


namespace Domain.Configurations;

public static class DomainConfiguration
{
    public static MediatRServiceConfiguration ConfigureDomainBehaviors(this MediatRServiceConfiguration configuration)
    {
        configuration.AddOpenBehavior(typeof(TransactionalBehavior<,>));

        return configuration;
    }

    public static MediatRServiceConfiguration ConfigureDomainHandlers(this MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(typeof(SignUpCommandHandler).Assembly);

        return configuration;
    }

    #region configure services
    public static IServiceCollection ConfigureDomainServices(this IServiceCollection services)
    {
        return services;
    }
    #endregion

    #region configure mappers
    public static IMapperConfigurationExpression ConfigureDomainMappers(this IMapperConfigurationExpression configuration)
    {
        configuration.AddProfile<UserProfile>();

        return configuration;
    }
    #endregion
}
