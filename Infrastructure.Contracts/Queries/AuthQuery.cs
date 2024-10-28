using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Queries;

public class AuthQuery(
    string login,
    string password
)
    : IRequest<Guid>
{
    public string Login { get; private init; } = login;

    public string Password { get; private init; } = password;
}
