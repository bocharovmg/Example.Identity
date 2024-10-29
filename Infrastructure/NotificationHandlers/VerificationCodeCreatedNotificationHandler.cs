using MediatR;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.NotificationHandlers;
using Exemple.Identity.Infrastructure.Contracts.Notifications;
using Infrastructure.Contracts.Commands;
using System.Text.Json;
using Infrastructure.Contracts.Enums.Outbox;


namespace Exemple.Identity.Infrastructure.NotificationHandlers;

public class VerificationCodeCreatedNotificationHandler : IVerificationCodeCreatedNotificationHandler
{
    private readonly IMediator _mediator;

    public VerificationCodeCreatedNotificationHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(VerificationCodeCreatedNotification request, CancellationToken cancellationToken)
    {
        #region prepare message data
        var subject = request.VerificationField switch
        {
            Contracts.Enums.User.VerificationFieldType.Email => "Подтверждение email",
            Contracts.Enums.User.VerificationFieldType.AlternativeEmail => "Подтверждение дополнительного email",
            Contracts.Enums.User.VerificationFieldType.Password => "Восстановление пароля",
            _ => throw new NotImplementedException($"Unknown verification field {request.VerificationField}")
        };

        var message = $"Код подтверждения: {request.VerificationCode}";
        #endregion

        #region prepare payload
        var payloadObject = new
        {
            To = new string[] { request.Email },
            Subject = subject,
            Message = message
        };

        var payload = JsonSerializer.Serialize(payloadObject);
        #endregion

        var addOutboxMessageRequest = new AddOutboxMessageCommand(payload, MessageType.Email);

        await _mediator.Send(addOutboxMessageRequest, cancellationToken);
    }
}
