using System.ComponentModel.DataAnnotations;


namespace Api.Contracts.Requests.Outbox;

public class RemoveProcessedMessagesRequest
{
    [Required]
    public IEnumerable<Guid> MessageIds { get; init; } = Enumerable.Empty<Guid>();
}
