using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Commands;

public class SetupPasswordCommand(
    Guid userId,
    string password
) :
    IRequest<bool>
{
    public Guid UserId { get; private init; } = userId;

    public string Password { get; private init; } = password;
}
