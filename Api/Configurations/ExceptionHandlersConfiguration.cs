using Api.ExceptionHandlers;
using Api.ExceptionHandlers.User;


namespace Api.Configurations;

public static class ExceptionHandlersConfiguration
{
    public static IServiceCollection ConfigureExceptionHandlers(this IServiceCollection services)
    {
        services.RegisterUserExceptionHandlers();

        services.AddExceptionHandler<UnhandledExceptionHandler>();

        return services;
    }

    public static IServiceCollection RegisterUserExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<InvalidLoginOrPasswordExceptionHandler>();

        services.AddExceptionHandler<UserNotExistsExceptionHandler>();

        services.AddExceptionHandler<DuplicateUserExceptionHandler>();

        services.AddExceptionHandler<VerificationCodeIsExpiredExceptionHandler>();

        services.AddExceptionHandler<VerificationIsNotStartedExceptionHandler>();

        services.AddExceptionHandler<WrongVerificationCodeExceptionHandler>();

        return services;
    }
}
