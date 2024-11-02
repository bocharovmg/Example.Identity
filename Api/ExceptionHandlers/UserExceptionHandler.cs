using Infrastructure.Contracts.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;


namespace Api.ExceptionHandlers;

public class UserExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            DuplicateUserException => CreateProblemDetails(httpContext, (DuplicateUserException)exception),
            InvalidLoginOrPasswordException => CreateProblemDetails(httpContext, (InvalidLoginOrPasswordException)exception),
            UserNotExistsException => CreateProblemDetails(httpContext, (UserNotExistsException)exception),
            _ => null
        };

        if (problemDetails == null)
        {
            return false;
        }

        string problemDetailsJson = System.Text.Json.JsonSerializer.Serialize(problemDetails);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = problemDetails.Status ?? httpContext.Response.StatusCode;

        await httpContext.Response.WriteAsync(problemDetailsJson, cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblemDetails(in HttpContext httpContext, in DuplicateUserException exception)
    {
        var statusCode = StatusCodes.Status409Conflict;

        var reason = ReasonPhrases.GetReasonPhrase(statusCode);

        return new ProblemDetails
        {
            Status = statusCode,
            Title = reason,
            Detail = exception.Message
        };
    }

    private ProblemDetails CreateProblemDetails(in HttpContext httpContext, in InvalidLoginOrPasswordException exception)
    {
        var statusCode = StatusCodes.Status401Unauthorized;

        var reason = ReasonPhrases.GetReasonPhrase(statusCode);

        return new ProblemDetails
        {
            Status = statusCode,
            Title = reason,
            Detail = exception.Message
        };
    }

    private ProblemDetails CreateProblemDetails(in HttpContext httpContext, in UserNotExistsException exception)
    {
        var statusCode = StatusCodes.Status404NotFound;

        var reason = ReasonPhrases.GetReasonPhrase(statusCode);

        return new ProblemDetails
        {
            Status = statusCode,
            Title = reason,
            Detail = exception.Message
        };
    }
}
