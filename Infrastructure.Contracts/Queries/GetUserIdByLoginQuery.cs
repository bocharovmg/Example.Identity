using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Queries;

public class GetUserIdByLoginQuery(
    string login
) :
    IRequest<Guid>
{
    public string Login { get; private init; } = login;
}
