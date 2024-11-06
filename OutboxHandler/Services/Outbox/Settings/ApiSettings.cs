using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OutboxHandler.Services.Outbox.Settings;

internal class ApiSettings
{
    [Required(AllowEmptyStrings = false)]
    public string Address { get; init; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string ApiKeyName { get; init; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string ApiKeyValue { get; init; } = string.Empty;

    [NotNull]
    public EndPoints EndPoints { get; init; } = null!;
}
