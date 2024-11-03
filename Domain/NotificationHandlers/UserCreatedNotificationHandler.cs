using MediatR;
using Domain.Contracts.Commands;
using Domain.Contracts.Enums.User;
using Domain.Contracts.Interfaces.NotificationHandlers;
using Domain.Contracts.Notifications;


namespace Domain.NotificationHandlers;

public class UserCreatedNotificationHandler : IUserCreatedNotificationHandler
{
    private readonly IMediator _mediator;

    public UserCreatedNotificationHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
    {
        var createVerificationCodeRequest = new CreateVerificationCodeCommand(
            notification.Email,
            VerificationFieldType.Email
        );

        await _mediator.Send(createVerificationCodeRequest, cancellationToken);
    }
}
