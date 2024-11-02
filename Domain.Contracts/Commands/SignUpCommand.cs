using Domain.Contracts.Interfaces.SeedWork;
using Domain.Contracts.Dtos;
using MediatR;


namespace Domain.Contracts.Commands;

public class SignUpCommand(
    string name,
    string email,
    string? alternativeEmail,
    string password
) :
    IRequest<AuthDto>,
    ITransactional
{
    public virtual string Name { get; private init; } = name;

    public virtual string Email { get; private init; } = email;

    public string? AlternativeEmail { get; private init; } = alternativeEmail;

    public virtual string Password { get; private init; } = password;
}
