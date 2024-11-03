using MediatR;


namespace Domain.Contracts.Notifications;

public class UserCreatedNotification(Guid userId, string name, string email, string? alternativeEmail) :
    INotification
{
    public Guid UserId { get; private init; } = userId;

    public string Name { get; private init; } = name;

    public string Email { get; private init; } = email;

    public string? AlternativeEmail { get; private init; } = alternativeEmail;
}
