namespace Infrastructure.Contracts.Exceptions;

public class InvalidLoginOrPasswordException : Exception
{
    public InvalidLoginOrPasswordException(string message) : base(message)
    { }
}
