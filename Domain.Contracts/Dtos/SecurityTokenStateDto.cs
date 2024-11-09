using Domain.Contracts.Enums.Jwt;


namespace Domain.Contracts.Dtos;

public class SecurityTokenStateDto
{
    public TokenValidationState State { get; init; }

    public Guid? UserId { get; init; }

    public string? Email { get; init; } = string.Empty;
}
