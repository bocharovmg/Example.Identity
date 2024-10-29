namespace Infrastructure.Contracts.Dtos;

public class OutboxMessageDto
{
    public Guid MessageId { get; init; }

    public string Payload { get; init; } = string.Empty;
}
