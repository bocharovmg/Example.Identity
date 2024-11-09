using MediatR;
using Domain.Contracts.Dtos;


namespace Domain.Contracts.Queries;

public class GetSecurityTokenStateQuery(string securityToken) : IRequest<SecurityTokenStateDto>
{
    public virtual string SecurityToken { get; private init; } = securityToken;
}
