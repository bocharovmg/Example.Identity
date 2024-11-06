using System.Text.Json;
using OutboxHandler.Processors.Email.Models;
using OutboxHandler.Interfaces;
using OutboxHandler.Contracts.Enums;


namespace OutboxHandler.Processors.Email;

internal class EmailOutboxProcessor : IEmailOutboxProcessor
{
    private ILogger<EmailOutboxProcessor> _logger;

    private readonly IOutboxService _outboxService;

    private readonly IEmailService _emailService;

    public EmailOutboxProcessor(
        ILogger<EmailOutboxProcessor> logger,
        IOutboxService outboxService,
        IEmailService emailService
    )
    {
        _logger = logger;

        _outboxService = outboxService;

        _emailService = emailService;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        while (true)
        {
            try
            {
                var outboxMessage = await _outboxService.NextMessagesAsync(MessageType.Email, cancellationToken);

                if (outboxMessage == null)
                {
                    break;
                }

                try
                {
                    var emailMessage = JsonSerializer.Deserialize<EmailMessageModel>(outboxMessage.Payload);

                    if (emailMessage != null)
                    {
                        #region send message
                        await _emailService
                            .SendMessageAsync(
                                emailMessage.To,
                                emailMessage.Subject,
                                emailMessage.Message,
                                emailMessage.From,
                                cancellationToken
                            );
                        #endregion
                    }

                    await _outboxService.SetMessageProcessedAsync(outboxMessage.MessageId, true, null, cancellationToken);
                }
                catch
                {
                    await _outboxService.SetMessageProcessedAsync(outboxMessage.MessageId, false, null, cancellationToken);

                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, null);
            }
        }
    }
}
