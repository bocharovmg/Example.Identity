namespace Exemple.Identity.Infrastructure.Contracts.Exceptions;

public class VerificationCodeNotExistsException : Exception
{
    public VerificationCodeNotExistsException(string message) : base(message) { }
}
