using Infrastructure.Contracts.Exceptions;


namespace Api.ExceptionHandlers.User;

public class UserNotExistsExceptionHandler : BaseExceptionHandler<UserNotExistsException>
{
    protected override int StatusCode => StatusCodes.Status404NotFound;
}
