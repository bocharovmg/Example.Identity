namespace Exemple.Identity.Infrastructure.Contracts.Exceptions;

public class UserNotExistsException : Exception
{
    public UserNotExistsException(string message) : base(message)
    { }
}
