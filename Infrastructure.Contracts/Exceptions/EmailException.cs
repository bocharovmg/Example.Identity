namespace Exemple.Identity.Infrastructure.Contracts.Exceptions;

public class EmailException : Exception
{
    public EmailException(string message, Exception innerException) : base(message, innerException) { }
}
