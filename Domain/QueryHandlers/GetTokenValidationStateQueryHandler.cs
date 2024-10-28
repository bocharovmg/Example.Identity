using Exemple.Identity.Domain.Contracts.Enums.Jwt;
using Exemple.Identity.Domain.Contracts.Interfaces.QueryHandlers;
using Exemple.Identity.Domain.Contracts.Queries;
using Exemple.Identity.Domain.Models;
using Exemple.Identity.Domain.Security;
using Microsoft.Extensions.Options;


namespace Exemple.Identity.Domain.QueryHandlers;

public class GetTokenValidationStateQueryHandler : IGetTokenValidationStateQueryHandler
{
    private readonly JwtOptions _jwtOptions;

    public GetTokenValidationStateQueryHandler(
        IOptions<JwtOptions> options
    )
    {
        _jwtOptions = options.Value ?? throw new ArgumentNullException($"{nameof(options)} of type {typeof(IOptions<JwtOptions>)}");
    }

    public async Task<TokenValidationState> Handle(GetTokenValidationStateQuery request, CancellationToken cancellationToken)
    {
        await Task.Yield();

        var securityToken = new SecurityTokenModel(_jwtOptions, request.SecurityToken);

        return securityToken.ValidationState;
    }
}
