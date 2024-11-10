using Domain.Contracts.Exceptions;


namespace Api.ExceptionHandlers.User;

public class VerificationIsNotStartedExceptionHandler : BaseExceptionHandler<VerificationIsNotStartedException>
{
    protected override int StatusCode => StatusCodes.Status409Conflict;
}
