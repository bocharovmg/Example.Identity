namespace Domain.Contracts.Exceptions;

public class UserIsVerifiedException : Exception
{
    public UserIsVerifiedException(string message) : base(message) { }
}
