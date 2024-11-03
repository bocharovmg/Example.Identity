using MediatR;
using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Enums.User;


namespace Infrastructure.Contracts.Commands;

public class TryGenerateVerificationCodeCommand(
    string login,
    VerificationFieldType verificationField
) :
    IRequest<VerificationCodeDto>
{
    public string Login { get; private init; } = login;

    public VerificationFieldType VerificationField { get; private init; } = verificationField;
}
