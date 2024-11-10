using MediatR;
using Infrastructure.Contracts.Enums.User;


namespace Infrastructure.Contracts.Commands;

public class ConfirmVerificationCodeCommand(
    Guid userId,
    string verificationCode,
    VerificationFieldType verificationField
) :
    IRequest<bool>
{
    public Guid UserId { get; private init; } = userId;

    public string VerificationCode { get; private init; } = verificationCode;

    public VerificationFieldType VerificationField { get; private init; } = verificationField;
}
