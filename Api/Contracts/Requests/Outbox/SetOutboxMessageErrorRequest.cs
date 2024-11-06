namespace Api.Contracts.Requests.Outbox;

public class SetOutboxMessageErrorRequest
{
    public string Error { get; init; } = string.Empty;
}
