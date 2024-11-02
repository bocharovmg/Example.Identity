using Infrastructure.Contracts.Enums.User;
using MediatR;


namespace Infrastructure.Contracts.Commands;

public class GenerateVerificationCodeCommand(
    Guid userId,
    VerificationFieldType verificationField
) :
    IRequest<string>
{
    public Guid UserId { get; private init; } = userId;

    public VerificationFieldType VerificationField { get; private init; } = verificationField;
}
