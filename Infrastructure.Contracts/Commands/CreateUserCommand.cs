using MediatR;


namespace Infrastructure.Contracts.Commands;

public class CreateUserCommand(
    string name,
    string email,
    string? alternativeEmail,
    string password
)
    : IRequest<Guid>
{
    public string Name { get; private init; } = name;

    public string Email { get; private init; } = email;

    public string? AlternativeEmail { get; private init; } = alternativeEmail;

    public string Password { get; private init; } = password;
}
