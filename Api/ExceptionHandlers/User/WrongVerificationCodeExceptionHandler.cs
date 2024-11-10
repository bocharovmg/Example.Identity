using Domain.Contracts.Exceptions;


namespace Api.ExceptionHandlers.User;

public class WrongVerificationCodeExceptionHandler : BaseExceptionHandler<WrongVerificationCodeException>
{
    protected override int StatusCode => StatusCodes.Status409Conflict;
}
