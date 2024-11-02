using Domain.Contracts.Dtos;
using MediatR;
using System.Security.Claims;


namespace Domain.Contracts.Queries;

public class GetSecurityTokenQuery : IRequest<SecurityTokenDto>
{
    public virtual Claim[] Claims { get; private init; }

    public GetSecurityTokenQuery(Claim[] claims)
    {
        Claims = claims;
    }
}
