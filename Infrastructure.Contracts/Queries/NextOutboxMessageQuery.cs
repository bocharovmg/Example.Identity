using MediatR;
using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Enums.Outbox;


namespace Infrastructure.Contracts.Queries
{
    public class NextOutboxMessageQuery(MessageType messageType) : IRequest<OutboxMessageDto?>
    {
        public MessageType MessageType { get; init; } = messageType;
    }
}
