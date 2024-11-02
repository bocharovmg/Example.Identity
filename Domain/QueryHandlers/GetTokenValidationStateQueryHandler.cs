using Domain.Contracts.Enums.Jwt;
using Domain.Contracts.Interfaces.QueryHandlers;
using Domain.Contracts.Queries;
using Domain.Models;
using Domain.Security;
using Microsoft.Extensions.Options;


namespace Domain.QueryHandlers;

public class GetTokenValidationStateQueryHandler : IGetTokenValidationStateQueryHandler
{
    private readonly JwtOptions _jwtOptions;

    public GetTokenValidationStateQueryHandler(
        IOptions<JwtOptions> options
    )
    {
        _jwtOptions = options.Value ?? throw new ArgumentNullException($"{nameof(options)} of type {typeof(IOptions<JwtOptions>)}");
    }

    public Task<TokenValidationState> Handle(GetTokenValidationStateQuery request, CancellationToken cancellationToken)
    {
        var securityToken = new SecurityTokenModel(_jwtOptions, request.SecurityToken);

        return Task.FromResult(securityToken.ValidationState);
    }
}
