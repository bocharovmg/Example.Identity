namespace Domain.Contracts.Exceptions;

public class WrongVerificationCodeException : Exception
{
    public WrongVerificationCodeException(string message) : base(message) { }
}
