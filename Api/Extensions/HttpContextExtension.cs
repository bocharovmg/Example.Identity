using Exemple.Identity.Infrastructure.Contracts;


namespace Exemple.Identity.Api.Extensions;

public static class HttpContextExtension
{
    public static UserContext BuildUserContext(this HttpContext httpContext)
    {
        if (!httpContext.Request.Cookies.TryGetValue("UserId", out var userId))
        {
            return new UserContext();
        }

        if (!httpContext.Request.Cookies.TryGetValue("Token", out var securityToken))
        {
            return new UserContext();
        }

        var userContext = new UserContext(Guid.Parse(userId), securityToken);

        return userContext;
    }
}
