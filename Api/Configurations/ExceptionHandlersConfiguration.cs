using Api.ExceptionHandlers;


namespace Api.Configurations;

public static class ExceptionHandlersConfiguration
{
    public static IServiceCollection ConfigureExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<UserExceptionHandler>();

        return services;
    }
}
