using Exemple.Identity.Domain.Contracts.Dtos;
using Exemple.Identity.Domain.Contracts.Interfaces.QueryHandlers;
using Exemple.Identity.Domain.Contracts.Queries;
using Exemple.Identity.Domain.Models;
using Exemple.Identity.Domain.Security;
using Microsoft.Extensions.Options;


namespace Exemple.Identity.Domain.QueryHandlers;

public class GetSecurityTokenQueryHandler : IGetSecurityTokenQueryHandler
{
    private readonly JwtOptions _jwtOptions;

    public GetSecurityTokenQueryHandler(
        IOptions<JwtOptions> options
    )
    {
        _jwtOptions = options.Value ?? throw new ArgumentNullException($"{nameof(options)} of type {typeof(IOptions<JwtOptions>)}");
    }

    public async Task<SecurityTokenDto> Handle(GetSecurityTokenQuery request, CancellationToken cancellationToken)
    {
        await Task.Yield();

        var securityToken = new SecurityTokenModel(_jwtOptions, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256, request.Claims);

        return new SecurityTokenDto
        {
            SecurityToken = securityToken.ToString()
        };
    }
}
