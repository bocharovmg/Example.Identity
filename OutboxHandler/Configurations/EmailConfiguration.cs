using OutboxHandler.Interfaces;
using OutboxHandler.Services.Email;
using OutboxHandler.Services.Email.Settings;


namespace OutboxHandler.Configurations;

internal static class EmailConfiguration
{
    public static IServiceCollection ConfigureEmailOptions(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile("email-config.json");

        services.Configure<EmailSettings>(configurationManager);

        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
