using Domain.Contracts.Enums.Jwt;
using MediatR;


namespace Domain.Contracts.Queries;

public class GetTokenValidationStateQuery(string securityToken) : IRequest<TokenValidationState>
{
    public virtual string SecurityToken { get; private init; } = securityToken;
}
