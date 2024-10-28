using Exemple.Identity.Domain.Contracts.Dtos;
using MediatR;


namespace Exemple.Identity.Domain.Contracts.Queries;

public class SignInQuery(
    string login,
    string password
) :
    IRequest<AuthDto>
{
    public virtual string Login { get; private init; } = login;

    public virtual string Password { get; private init; } = password;
}
