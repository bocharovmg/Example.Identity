using MediatR;


namespace Domain.Contracts.Commands;

public class ConfirmEmailCommand(
    string email,
    string verificationCode,
    bool isAlternativeEmail
) :
    IRequest
{
    public string Email { get; private init; } = email;

    public string VerificationCode { get; private init; } = verificationCode;

    public bool IsAlternativeEmail { get; private init; } = isAlternativeEmail;
}
