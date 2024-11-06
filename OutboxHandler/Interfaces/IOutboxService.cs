using OutboxHandler.Contracts.Dtos;
using OutboxHandler.Contracts.Enums;


namespace OutboxHandler.Interfaces;

internal interface IOutboxService
{
    Task<OutboxMessageDto?> NextMessagesAsync(MessageType messageType, CancellationToken cancellationToken = default);

    Task<bool> SetMessageProcessedAsync(Guid messageId, bool isSuccess, string? error = null, CancellationToken cancellationToken = default);
}
