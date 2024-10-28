using Exemple.Identity.Domain.Contracts.Dtos;
using MediatR;
using System.Security.Claims;


namespace Exemple.Identity.Domain.Contracts.Queries;

public class GetSecurityTokenQuery : IRequest<SecurityTokenDto>
{
    public virtual Claim[] Claims { get; private init; }

    public GetSecurityTokenQuery(Claim[] claims)
    {
        Claims = claims;
    }
}
