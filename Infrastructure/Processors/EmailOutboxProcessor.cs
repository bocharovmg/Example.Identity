using MediatR;
using Infrastructure.Contracts.Interfaces.Services;
using Infrastructure.Contracts.Queries;
using Infrastructure.Contracts.Enums.Outbox;
using Microsoft.Extensions.Logging;
using Infrastructure.Processors.Models;
using System.Text.Json;
using Infrastructure.Contracts.Commands;


namespace Infrastructure.Processors;

public class EmailOutboxProcessor : IEmailOutboxProcessor
{
    private ILogger<EmailOutboxProcessor> _logger;

    private readonly IMediator _mediator;

    private readonly IEmailService _emailService;

    public EmailOutboxProcessor(
        ILogger<EmailOutboxProcessor> logger,
        IMediator mediator,
        IEmailService emailService
    )
    {
        _logger = logger;

        _mediator = mediator;

        _emailService = emailService;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        while(true)
        {
            try
            {
                #region get message
                var nextOutboxMessageRequest = new NextOutboxMessageQuery(MessageType.Email);

                var outboxMessage = await _mediator.Send(nextOutboxMessageRequest, cancellationToken);
                #endregion

                if (outboxMessage == null)
                {
                    break;
                }

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

                #region remove message
                var removeOutboxMessageRequest = new RemoveOutboxMessageCommand(outboxMessage.MessageId);

                await _mediator.Send(removeOutboxMessageRequest, cancellationToken);
                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, null);
            }
        } 
    }
}
