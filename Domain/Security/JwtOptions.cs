﻿namespace Exemple.Identity.Domain.Security;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Secret { get; set; } = string.Empty;

    public int AccessTokenExpiration { get; set; }

    public int RefreshTokenExpiration { get; set; }
}