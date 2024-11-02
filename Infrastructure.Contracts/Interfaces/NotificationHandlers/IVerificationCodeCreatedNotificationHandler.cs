using MediatR;
using Infrastructure.Contracts.Notifications;


namespace Infrastructure.Contracts.Interfaces.NotificationHandlers;

public interface IVerificationCodeCreatedNotificationHandler : INotificationHandler<VerificationCodeCreatedNotification>;
