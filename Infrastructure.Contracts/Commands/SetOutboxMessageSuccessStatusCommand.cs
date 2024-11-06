using MediatR;


namespace Infrastructure.Contracts.Commands;

public class SetOutboxMessageSuccessStatusCommand(Guid messageId) : IRequest
{
    public Guid MessageId { get; private init; } = messageId;
}
