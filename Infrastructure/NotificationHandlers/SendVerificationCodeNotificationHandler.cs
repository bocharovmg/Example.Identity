using Exemple.Identity.Infrastructure.Contracts.Interfaces.NotificationHandlers;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.Services;
using Exemple.Identity.Infrastructure.Contracts.Notifications;


namespace Exemple.Identity.Infrastructure.NotificationHandlers;

public class SendVerificationCodeNotificationHandler : ISendVerificationCodeNotificationHandler
{
    private readonly IEmailService _emailService;

    public SendVerificationCodeNotificationHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(SendVerificationCodeNotification request, CancellationToken cancellationToken)
    {
        var subject = request.VerificationField switch
        {
            Contracts.Enums.User.VerificationFieldType.Email => "Подтверждение email",
            Contracts.Enums.User.VerificationFieldType.AlternativeEmail => "Подтверждение дополнительного email",
            Contracts.Enums.User.VerificationFieldType.Password => "Восстановление пароля",
            _ => throw new NotImplementedException($"Unknown verification field {request.VerificationField}")
        };

        var message = $"Код подтверждения: {request.VerificationCode}";

        await _emailService.SendMessageAsync(request.Email, subject, message, cancellationToken: cancellationToken);
    }
}
