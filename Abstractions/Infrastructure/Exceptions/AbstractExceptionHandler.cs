using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;


namespace Abstractions.Infrastructure.Exceptions;

public abstract class AbstractExceptionHandler<TException> : IExceptionHandler where TException : Exception
{
    public virtual async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            TException => CreateProblemDetails(httpContext, (TException)exception),
            _ => null
        };

        if (problemDetails == null)
        {
            return false;
        }

        await SetResponseAsync(httpContext, problemDetails, cancellationToken);

        return true;
    }

    protected async Task SetResponseAsync(HttpContext httpContext, ProblemDetails problemDetails, CancellationToken cancellationToken)
    {
        string problemDetailsJson = System.Text.Json.JsonSerializer.Serialize(problemDetails);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = problemDetails.Status ?? httpContext.Response.StatusCode;

        await httpContext.Response.WriteAsync(problemDetailsJson, cancellationToken);
    }

    protected abstract ProblemDetails CreateProblemDetails(in HttpContext httpContext, in TException exception);
}
