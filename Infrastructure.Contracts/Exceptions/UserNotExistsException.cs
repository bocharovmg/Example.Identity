namespace Infrastructure.Contracts.Exceptions;

public class UserNotExistsException : Exception
{
    public UserNotExistsException(string message) : base(message)
    { }
}
