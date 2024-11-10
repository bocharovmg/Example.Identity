namespace Domain.Contracts.Exceptions;

public class VerificationIsNotStartedException : Exception
{
    public VerificationIsNotStartedException(string message) : base(message) { }
}
