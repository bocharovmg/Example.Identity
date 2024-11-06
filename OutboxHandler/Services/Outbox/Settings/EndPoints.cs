using System.ComponentModel.DataAnnotations;

namespace OutboxHandler.Services.Outbox.Settings;

internal class EndPoints
{
    [Required(AllowEmptyStrings = false)]
    public string NextMessageEndPoint { get; init; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string MassageSuccessEndPoint { get; init; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string MassageErrorEndPoint { get; init; } = string.Empty;
}
