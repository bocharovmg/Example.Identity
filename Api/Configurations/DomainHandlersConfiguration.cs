using AutoMapper;
using Exemple.Identity.Abstractions.Core.CommandHandlers;
using Exemple.Identity.Domain.CommandHandlers;
using Exemple.Identity.Domain.Configurations.Profiles;
using Exemple.Identity.Domain.QueryHandlers;


namespace Exemple.Identity.Domain.Configurations;

public static class DomainHandlersConfiguration
{
    #region configure handlers
    public static MediatRServiceConfiguration ConfigureDomainHandlers(this MediatRServiceConfiguration configuration)
    {
        RegisterCammandHandlers(configuration);

        RegisterQueryHandlers(configuration);

        return configuration;
    }

    private static void RegisterCammandHandlers(MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(typeof(BeginChangesCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(ApplyChangesCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(ConfirmVerificationCodeCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(CreateAndSendVerificationCodeCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(SetupPasswordCommandCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(SignUpCommandHandler).Assembly);
    }

    private static void RegisterQueryHandlers(MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(typeof(SignInQueryHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(GetSecurityTokenQueryHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(GetTokenValidationStateQueryHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(GetVerificationStateQueryHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(GetUserQueryHandler).Assembly);
    }
    #endregion

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
