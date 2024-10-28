using Exemple.Identity.Domain.Contracts.Dtos;
using Exemple.Identity.Domain.Contracts.Enums.User;
using MediatR;


namespace Exemple.Identity.Domain.Contracts.Commands;

public class CreateAndSendVerificationCodeCommand(string email, VerificationFieldType verificationField) : IRequest<VerificationStateDto>
{
    public string Email { get; private init; } = email;

    public VerificationFieldType VerificationField { get; private init; } = verificationField;
}
