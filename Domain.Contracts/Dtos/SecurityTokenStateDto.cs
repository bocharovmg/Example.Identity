using Exemple.Identity.Domain.Contracts.Enums.Jwt;


namespace Exemple.Identity.Domain.Contracts.Dtos;

public class SecurityTokenStateDto
{
    public TokenValidationState State { get; init; }
}
