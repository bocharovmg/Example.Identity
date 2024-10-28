namespace Exemple.Identity.Domain.Contracts.Dtos;

public class SecurityTokenDto
{
    public string SecurityToken { get; init; } = string.Empty;

    public string RefreshToken { get; init; } = string.Empty;
}
