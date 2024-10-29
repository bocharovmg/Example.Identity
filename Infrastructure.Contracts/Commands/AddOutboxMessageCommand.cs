using MediatR;
using Infrastructure.Contracts.Enums.Outbox;


namespace Infrastructure.Contracts.Commands;

public class AddOutboxMessageCommand(string payload, MessageType messageType) : IRequest
{
    public string Payload { get; init; } = payload;

    public MessageType MessageType { get; init; } = messageType;
}
