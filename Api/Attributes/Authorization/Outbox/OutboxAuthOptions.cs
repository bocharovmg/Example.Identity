using System.ComponentModel.DataAnnotations;


namespace Api.Attributes.Authorization.Outbox;

public class OutboxAuthOptions
{
    [Required(AllowEmptyStrings = false)]
    public string ApiKeyName { get; init; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string ApiKeyValue { get; init; } = string.Empty;
}
