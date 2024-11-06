namespace OutboxHandler.Contracts.Dtos;

internal class OutboxMessageDto
{
    public Guid MessageId { get; init; }

    public string Payload { get; init; } = string.Empty;
}
