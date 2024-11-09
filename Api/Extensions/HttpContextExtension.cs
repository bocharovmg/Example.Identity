using MediatR;
using Domain.Contracts.Dtos;
using Domain.Contracts.Enums.Jwt;
using Domain.Contracts.Queries;
using Infrastructure.Contracts;


namespace Api.Extensions;

public static class HttpContextExtension
{
    public static UserContext BuildUserContext(this HttpContext httpContext)
    {
        if (!httpContext.Request.Cookies.TryGetValue("Token", out var securityToken))
        {
            return new UserContext();
        }

        var securityTokenState = GetSecurityTokenState(httpContext, securityToken);

        if (securityTokenState.State != TokenValidationState.Valid)
        {
            return new UserContext();
        }

        if (securityTokenState.UserId == null)
        {
            throw new ArgumentNullException(nameof(securityTokenState.UserId));
        }

        if (securityTokenState.Email == null)
        {
            throw new ArgumentNullException(nameof(securityTokenState.Email));
        }

        var userContext = new UserContext(securityTokenState.UserId.Value, securityTokenState.Email, securityToken);

        return userContext;
    }

    private static SecurityTokenStateDto GetSecurityTokenState(HttpContext httpContext, string securityToken)
    {
        var mediator = httpContext.RequestServices.GetRequiredService<IMediator>();

        var getSecurityTokenStateRequest = new GetSecurityTokenStateQuery(securityToken);

        var securityTokenStateTask = mediator.Send(getSecurityTokenStateRequest);

        securityTokenStateTask.Wait();

        return securityTokenStateTask.Result;
    }
}
