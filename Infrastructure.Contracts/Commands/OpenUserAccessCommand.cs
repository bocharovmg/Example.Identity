using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Commands;

public class OpenUserAccessCommand(
    Guid userId
) :
    IRequest<bool>
{
    public Guid UserId { get; private init; } = userId;
}
