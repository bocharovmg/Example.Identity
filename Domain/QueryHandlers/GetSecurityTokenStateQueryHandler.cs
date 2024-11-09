using Domain.Contracts.Dtos;
using Domain.Contracts.Interfaces.QueryHandlers;
using Domain.Contracts.Queries;
using Domain.Models;
using Domain.Security;
using Microsoft.Extensions.Options;


namespace Domain.QueryHandlers;

public class GetSecurityTokenStateQueryHandler : IGetSecurityTokenStateQueryHandler
{
    private readonly JwtOptions _jwtOptions;

    public GetSecurityTokenStateQueryHandler(
        IOptions<JwtOptions> options
    )
    {
        _jwtOptions = options.Value ?? throw new ArgumentNullException($"{nameof(options)} of type {typeof(IOptions<JwtOptions>)}");
    }

    public Task<SecurityTokenStateDto> Handle(GetSecurityTokenStateQuery request, CancellationToken cancellationToken)
    {
        var securityToken = new SecurityTokenModel(_jwtOptions, request.SecurityToken);

        var userId = securityToken
            .Claims
            .Where(claim => claim.Type == nameof(GetSecurityTokenQuery.UserId))
            .Select(claim => {
                Guid? result = null;

                result = Guid.Parse(claim.Value);

                return result;
            })
            .FirstOrDefault();

        var email = securityToken
            .Claims
            .Where(claim => claim.Type == nameof(GetSecurityTokenQuery.Email))
            .Select(claim => claim.Value)
            .FirstOrDefault();

        return Task.FromResult(new SecurityTokenStateDto
        {
            State = securityToken.ValidationState,
            UserId = userId,
            Email = email
        });
    }
}
