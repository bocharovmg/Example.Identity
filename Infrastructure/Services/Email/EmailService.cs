using System.Net;
using System.Net.Mail;
using Infrastructure.Contracts.Exceptions;
using Infrastructure.Contracts.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Infrastructure.Services.Email;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    private readonly EmailSettings _settings;

    public EmailService(
        ILogger<EmailService> logger,
        IOptions<EmailSettings> settings
    )
    {
        _logger = logger;

        _settings = settings.Value ?? throw new ArgumentNullException($"{nameof(settings)} of type {typeof(IOptions<EmailSettings>)}");
    }

    /// <summary>
    /// Send email message
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessageAsync(
        MailMessage message,
        CancellationToken cancellationToken = default
    )
    {
        using var smtpClient = new SmtpClient
        {
            Host = _settings.Host,
            Port = _settings.Port,
            UseDefaultCredentials = _settings.UseDefaultCredentials ?? false,
            EnableSsl = _settings.EnableSsl ?? true,
            Timeout = _settings.Timeout ?? 100000,
        };

        try
        {
            smtpClient.Credentials = new NetworkCredential(
                _settings.Login,
                _settings.Password
            );

            await smtpClient.SendMailAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            var exceptionMessage = "Failed to send email message";

            _logger.LogError(new EmailException("", ex), exceptionMessage);
        }
    }

    /// <summary>
    /// Send email message
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessageAsync(
        IEnumerable<string> to,
        string subject,
        string message,
        string? from = null,
        CancellationToken cancellationToken = default
    )
    {
        using var mailMessage = new MailMessage
        {
            From = new MailAddress(from ?? _settings.Login),
            Subject = subject,
            Body = message
        };

        foreach (var recipient in to)
        {
            mailMessage.To.Add(recipient);
        }

        await SendMessageAsync(mailMessage, cancellationToken);
    }
}
