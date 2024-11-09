using MediatR;
using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Enums.User;


namespace Infrastructure.Contracts.Commands;

public class TryGenerateVerificationCodeCommand(
    Guid userId,
    VerificationFieldType verificationField
) :
    IRequest<VerificationCodeDto>
{
    public Guid UserId { get; private init; } = userId;

    public VerificationFieldType VerificationField { get; private init; } = verificationField;
}
