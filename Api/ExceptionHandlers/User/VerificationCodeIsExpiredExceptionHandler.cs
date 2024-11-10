using Domain.Contracts.Exceptions;


namespace Api.ExceptionHandlers.User;

public class VerificationCodeIsExpiredExceptionHandler : BaseExceptionHandler<VerificationCodeIsExpiredException>
{
    protected override int StatusCode => StatusCodes.Status409Conflict;
}
