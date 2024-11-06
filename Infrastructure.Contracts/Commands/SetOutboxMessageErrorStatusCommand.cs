using MediatR;


namespace Infrastructure.Contracts.Commands;

public class SetOutboxMessageErrorStatusCommand(Guid messageId, string? error) : IRequest
{
    public Guid MessageId { get; private init; } = messageId;

    public string? MessageError { get; private init; } = error;
}
