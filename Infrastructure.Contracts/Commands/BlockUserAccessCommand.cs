using Exemple.Identity.Infrastructure.Contracts.Enums.User;
using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Commands;

public class BlockUserAccessCommand(
    Guid userId,
    BlockType block,
    string reason,
    DateTime blockUntil
) :
    IRequest<bool>
{
    public Guid UserId { get; private init; } = userId;

    public BlockType Block { get; private init; } = block;

    public string Reason { get; private init; } = reason;

    public DateTime BlockUntil { get; private init; } = blockUntil;
}
