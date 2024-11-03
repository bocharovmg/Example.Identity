using MediatR;
using Domain.Contracts.Notifications;


namespace Domain.Contracts.Interfaces.NotificationHandlers;

public interface IUserCreatedNotificationHandler : INotificationHandler<UserCreatedNotification>;
