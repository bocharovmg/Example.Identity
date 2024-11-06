using System.Net.Mail;


namespace OutboxHandler.Interfaces;

internal interface IEmailService
{
    /// <summary>
    /// Send email message
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendMessageAsync(MailMessage message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email message
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendMessageAsync(IEnumerable<string> to, string subject, string message, string? from = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email message
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    async Task SendMessageAsync(string to, string subject, string message, string? from = null, CancellationToken cancellationToken = default)
    {
        await SendMessageAsync([to], subject, message, from, cancellationToken);
    }
}
