namespace Api.ExceptionHandlers;

public class UnhandledExceptionHandler : BaseExceptionHandler<Exception>
{
    public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = CreateProblemDetails(httpContext, exception);

        await SetResponseAsync(httpContext, problemDetails, cancellationToken);

        return true;
    }

    protected override int StatusCode => StatusCodes.Status500InternalServerError;
}
