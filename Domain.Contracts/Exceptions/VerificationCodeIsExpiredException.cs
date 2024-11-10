namespace Domain.Contracts.Exceptions;

public class VerificationCodeIsExpiredException : Exception
{
    public VerificationCodeIsExpiredException(string message) : base(message) { }
}
