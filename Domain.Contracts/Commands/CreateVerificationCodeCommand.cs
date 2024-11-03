using Domain.Contracts.Dtos;
using Domain.Contracts.Enums.User;
using Domain.Contracts.Interfaces.SeedWork;
using MediatR;


namespace Domain.Contracts.Commands;

public class CreateVerificationCodeCommand(string email, VerificationFieldType verificationField) : IRequest<VerificationStateDto>, ITransactional
{
    public string Email { get; private init; } = email;

    public VerificationFieldType VerificationField { get; private init; } = verificationField;
}
