using MediatR;


namespace Infrastructure.Contracts.Commands;

public class RemoveOutboxMessageCommand(Guid messageId) : IRequest
{
    public Guid MessageId { get; init; } = messageId;
}
