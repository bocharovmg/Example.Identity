using Domain.Contracts.Dtos;
using Domain.Contracts.Interfaces.QueryHandlers;
using Domain.Contracts.Queries;
using Domain.Models;
using Domain.Security;
using Microsoft.Extensions.Options;


namespace Domain.QueryHandlers;

public class GetSecurityTokenQueryHandler : IGetSecurityTokenQueryHandler
{
    private readonly JwtOptions _jwtOptions;

    public GetSecurityTokenQueryHandler(
        IOptions<JwtOptions> options
    )
    {
        _jwtOptions = options.Value ?? throw new ArgumentNullException($"{nameof(options)} of type {typeof(IOptions<JwtOptions>)}");
    }

    public Task<SecurityTokenDto> Handle(GetSecurityTokenQuery request, CancellationToken cancellationToken)
    {
        var securityToken = new SecurityTokenModel(_jwtOptions, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256, request.Claims);

        return Task.FromResult(new SecurityTokenDto
        {
            SecurityToken = securityToken.ToString()
        });
    }
}
