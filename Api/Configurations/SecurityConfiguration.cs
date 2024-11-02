using Domain.Security;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Api.Configurations;

public static class SecurityConfiguration
{
    public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile("jwt-config.json");

        services.Configure<JwtOptions>(configurationManager);

        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        authenticationBuilder
            .AddJwtBearer(
                "Bearer-default",
                options =>
                {
                    var jwtOptions = new JwtOptions();

                    configurationManager.Bind(jwtOptions);

                    if (string.IsNullOrWhiteSpace(jwtOptions.Secret))
                    {
                        throw new NullReferenceException($"Missing Secret key of the Jwt Token configuration [{nameof(jwtOptions.Secret)}]");
                    }

                    options.Audience = jwtOptions.Audience;
                    options.Authority = null;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        RequireExpirationTime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var userContext = context.HttpContext.RequestServices.GetRequiredService<UserContext>();

                            context.Token = userContext.SecurityToken;

                            return Task.CompletedTask;
                        }
                    };
                }
            );

        authenticationBuilder.AddPolicyScheme(
            JwtBearerDefaults.AuthenticationScheme,
            "Selector",
            options =>
            {
                options.ForwardDefaultSelector = context => "Bearer-default";
            }
        );

        services
            .AddAuthorization(options =>
            {
                options
                    .AddPolicy("MyPolicy", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

        return services;
    }
}
