using MediatR;


namespace Infrastructure.Contracts.Queries;

public class GetUserIdByLoginQuery(
    string login
) :
    IRequest<Guid>
{
    public string Login { get; private init; } = login;
}
