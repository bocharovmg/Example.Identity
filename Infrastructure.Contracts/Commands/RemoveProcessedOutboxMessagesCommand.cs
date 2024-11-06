using MediatR;


namespace Infrastructure.Contracts.Commands;

public class RemoveProcessedOutboxMessagesCommand(IEnumerable<Guid> messageIds) : IRequest
{
    public IEnumerable<Guid> MessageIds { get; private init; } = messageIds;
}
