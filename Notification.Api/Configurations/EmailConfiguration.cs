using Exemple.Identity.Infrastructure.Contracts.Interfaces.Services;
using Exemple.Identity.Infrastructure.Services.Email;


namespace Notification.Api.Configurations;

public static class EmailConfiguration
{
    public static IServiceCollection ConfigureEmailOptions(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile("email-config.json");

        services.Configure<EmailSettings>(configurationManager);

        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
