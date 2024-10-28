using Exemple.Identity.Infrastructure.Services.Email;


namespace Exemple.Identity.Api.Configurations;

public static class EmailConfiguration
{
    public static IServiceCollection ConfigureEmailOptions(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile("email-config.json");

        services.Configure<EmailSettings>(configurationManager);

        return services;
    }
}
