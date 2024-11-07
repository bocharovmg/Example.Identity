using Abstractions.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;


namespace Api.ExceptionHandlers;

public abstract class BaseExceptionHandler<TException> : AbstractExceptionHandler<TException> where TException : Exception
{
    protected abstract int StatusCode { get; }

    protected override ProblemDetails CreateProblemDetails(in HttpContext httpContext, in TException exception)
    {
        var reason = ReasonPhrases.GetReasonPhrase(StatusCode);

        return new ProblemDetails
        {
            Status = StatusCode,
            Title = reason,
            Detail = exception.Message
        };
    }
}
