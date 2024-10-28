using MediatR;
using Exemple.Identity.Infrastructure.Contracts.Notifications;


namespace Exemple.Identity.Infrastructure.Contracts.Interfaces.NotificationHandlers;

public interface IVerificationCodeCreatedNotificationHandler : INotificationHandler<VerificationCodeCreatedNotification>;
