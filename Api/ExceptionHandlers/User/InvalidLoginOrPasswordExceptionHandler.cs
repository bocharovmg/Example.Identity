using Infrastructure.Contracts.Exceptions;


namespace Api.ExceptionHandlers.User;

public class InvalidLoginOrPasswordExceptionHandler : BaseExceptionHandler<InvalidLoginOrPasswordException>
{
    protected override int StatusCode => StatusCodes.Status401Unauthorized;
}
