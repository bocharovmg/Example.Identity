namespace Exemple.Identity.Infrastructure.Services.Email;

public class EmailSettings
{
    /// <summary>
    /// Host
    /// </summary>
    public string Host { get; init; } = string.Empty;

    /// <summary>
    /// Port
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Login
    /// </summary>
    public string Login { get; init; } = string.Empty;

    /// <summary>
    /// Password
    /// </summary>
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
