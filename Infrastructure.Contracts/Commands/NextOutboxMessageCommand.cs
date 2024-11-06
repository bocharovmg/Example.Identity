using MediatR;
using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Enums.Outbox;


namespace Infrastructure.Contracts.Commands
{
    public class NextOutboxMessageCommand(MessageType messageType) : IRequest<OutboxMessageDto?>
    {
        public MessageType MessageType { get; private init; } = messageType;
    }
}
