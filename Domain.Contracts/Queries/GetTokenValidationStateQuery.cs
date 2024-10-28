using Exemple.Identity.Domain.Contracts.Enums.Jwt;
using MediatR;


namespace Exemple.Identity.Domain.Contracts.Queries;

public class GetTokenValidationStateQuery(string securityToken) : IRequest<TokenValidationState>
{
    public virtual string SecurityToken { get; private init; } = securityToken;
}
