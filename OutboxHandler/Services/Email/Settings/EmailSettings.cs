using System.ComponentModel.DataAnnotations;


namespace OutboxHandler.Services.Email.Settings;

internal class EmailSettings
{
    /// <summary>
    /// Host
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Host { get; init; } = string.Empty;

    /// <summary>
    /// Port
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public int Port { get; init; }

    /// <summary>
    /// Login
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Login { get; init; } = string.Empty;

    /// <summary>
    /// Password
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Password { get; init; } = string.Empty;

    /// <summary>
    /// Use default credentials
    /// </summary>
    public bool? UseDefaultCredentials { get; init; }

    /// <summary>
    /// Enable ssl
    /// </summary>
    public bool? EnableSsl { get; init; }

    /// <summary>
    /// Timeout
    /// </summary>
    public int? Timeout { get; init; }
}
