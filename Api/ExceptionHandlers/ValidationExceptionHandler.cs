using FluentValidation;


namespace Api.ExceptionHandlers;

public class ValidationExceptionHandler : BaseExceptionHandler<ValidationException>
{
    protected override int StatusCode => StatusCodes.Status409Conflict;
}
