using AutoMapper;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.CommandHandlers;
using Infrastructure.Configuration.Profiles;
using Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Interfaces.Services;
using Infrastructure.NotificationHandlers;
using Infrastructure.Services;
using Infrastructure.CommandHandlers;


namespace Infrastructure.Configuration;

public static class InfrastructureConfiguration
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
        configuration.RegisterServicesFromAssembly(typeof(VerificationCodeCreatedNotificationHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(BlockUserAccessCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(ConfirmVerificationCodeCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(TryGenerateVerificationCodeCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(OpenUserAccessCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(SetupPasswordCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(SetupUserLanguageCommandHandler).Assembly);

        configuration.RegisterServicesFromAssembly(typeof(AddOutboxMessageCommandHandler).Assembly);
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
        services.AddSingleton<IVerificationStateLifetimeService, VerificationStateLifetimeService>();

        services.AddScoped<ISqlConnectionManager, SqlConnectionManager>();

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
