using AutoMapper;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using Exemple.Identity.Infrastructure.CommandHandlers;
using Exemple.Identity.Infrastructure.Configuration.Profiles;
using Exemple.Identity.Infrastructure.ConnectionManager;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.Services;
using Exemple.Identity.Infrastructure.NotificationHandlers;
using Exemple.Identity.Infrastructure.Services;
using Exemple.Identity.Infrastructure.Services.Email;
using Microsoft.Extensions.DependencyInjection;


namespace Exemple.Identity.Infrastructure.Configuration;

public static class InfrastructureConfigurator
{
    #region configure handlers
    public static MediatRServiceConfiguration ConfigureInfrastructureHandlers(this MediatRServiceConfiguration configuration)
    {
        RegisterCammandHandlers(configuration);

        RegisterQueryHandlers(configuration);

        return configuration;
    }

    private static void RegisterCammandHandlers(MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(typeof(SendVerificationCodeNotificationHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(BlockUserAccessCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(ConfirmVerificationCodeCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(GenerateVerificationCodeCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(OpenUserAccessCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(SetupPasswordCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(SetupUserLanguageCommandHandler).Assembly);
    }

    private static void RegisterQueryHandlers(MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(typeof(AuthQueryHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(GetUserIdByLoginQueryHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(GetUserQueryHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(GetVerificationStateTypeQueryHandler).Assembly);
    }
    #endregion

    #region configure services
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services
    )
    {
        services.AddSingleton<IVerificationStateCache, VerificationStateCache>();

        services.AddScoped<ISqlConnectionManager, SqlConnectionManager>();

        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
    #endregion

    #region configure mappers
    public static IMapperConfigurationExpression ConfigureInfrastructureMappers(this IMapperConfigurationExpression configuration)
    {
        configuration.AddProfile<UserProfile>();

        return configuration;
    }
    #endregion
}
