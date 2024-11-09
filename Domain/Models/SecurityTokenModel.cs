using Domain.Contracts.Enums.Jwt;
using Domain.Security;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Domain.Models;

public sealed class SecurityTokenModel
{
    private readonly string _token;

    private readonly IReadOnlyCollection<Claim> _claims;

    public TokenValidationState ValidationState { get; private init; }

    public IReadOnlyCollection<Claim> Claims => _claims;

    public SecurityTokenModel(
        JwtOptions options,
        string algorithm,
        Claim[] claims
    )
    {
        _claims = claims;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret));

        var creds = new SigningCredentials(key, algorithm);

        var validFrom = DateTime.UtcNow;

        var tokenModel = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            notBefore: validFrom,
            claims: _claims,
            expires: validFrom.AddMinutes(options.AccessTokenExpiration),
            signingCredentials: creds
        );

        var securityTokenHandler = new JwtSecurityTokenHandler();

        _token = securityTokenHandler.WriteToken(tokenModel);

        ValidationState = TokenValidationState.Valid;
    }

    public SecurityTokenModel(
        JwtOptions options,
        string token
    )
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        var securityToken = jwtHandler.ReadJwtToken(token);

        var validationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = options.Audience,
            ValidateIssuer = true,
            ValidIssuer = options.Issuer,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Secret))
        };

        _token = string.Empty;

        _claims = new List<Claim>();

        try
        {
            var validationResult = jwtHandler.ValidateToken(token, validationParameters, out var _);

            _token = token;

            _claims = validationResult.Claims.ToList();

            ValidationState = TokenValidationState.Valid;
        }
        catch (SecurityTokenExpiredException)
        {
            ValidationState = TokenValidationState.Expired;
        }
        catch (Exception ex)
        {
            if (ex is SecurityTokenValidationException)
            {
                ValidationState = TokenValidationState.Invalid;
            }
            else
            {
                throw;
            }
        }
    }

    public object Value => ToString();

    public override string ToString()
    {
        return _token;
    }
}
