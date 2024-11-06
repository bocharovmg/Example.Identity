using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;


namespace Api.Attributes.Authorization.Outbox;

public class OutboxAuthAction : IAuthorizationFilter
{
    private readonly string _authHeaderName = "X-Api-Key";

    private readonly string _authKey = "xGMyLwCoM2q8HZYV5aJU1BddFdG2m9UgnzQprbp7XCRqR8XwZuC9qrFEbmQemCSHu9Qp18roMe67jUKU8TSVcd9kL3Y6h3vhWBv";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        using var scope = context.HttpContext.RequestServices.CreateScope();

        var outboxAuthOptions = scope.ServiceProvider.GetRequiredService<IOptions<OutboxAuthOptions>>().Value
            ?? throw new ArgumentNullException($"Failed to inject property of type {typeof(IOptions<OutboxAuthOptions>)}");

        if (!context.HttpContext.Request.Headers.TryGetValue(outboxAuthOptions.ApiKeyName, out var apiKeyValue))
        {
            context.Result = new UnauthorizedResult();

            return;
        }

        if (apiKeyValue != outboxAuthOptions.ApiKeyValue)
        {
            context.Result = new ForbidResult();

            return;
        }
    }
}
