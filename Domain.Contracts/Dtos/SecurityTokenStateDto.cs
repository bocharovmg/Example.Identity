using Domain.Contracts.Enums.Jwt;


namespace Domain.Contracts.Dtos;

public class SecurityTokenStateDto
{
    public TokenValidationState State { get; init; }
}
