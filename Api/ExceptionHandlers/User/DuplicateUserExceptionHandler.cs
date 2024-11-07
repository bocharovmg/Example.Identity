using Infrastructure.Contracts.Exceptions;


namespace Api.ExceptionHandlers.User;

public class DuplicateUserExceptionHandler : BaseExceptionHandler<DuplicateUserException>
{
    protected override int StatusCode => StatusCodes.Status409Conflict;
}
