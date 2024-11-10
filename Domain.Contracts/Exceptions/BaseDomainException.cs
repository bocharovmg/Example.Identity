namespace Domain.Contracts.Exceptions;

public class BaseDomainException : Exception
{
    public BaseDomainException(string message) : base(message) { }
}
