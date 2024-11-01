﻿using Exemple.Identity.Api.ExceptionHandlers;


namespace Exemple.Identity.Api.Configurations;

public static class ExceptionHandlersConfiguration
{
    public static IServiceCollection ConfigureExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<UserExceptionHandler>();

        return services;
    }
}
