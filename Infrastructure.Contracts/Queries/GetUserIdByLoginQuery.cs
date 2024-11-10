using MediatR;


namespace Infrastructure.Contracts.Queries;

/// <summary>
/// Get user id by login
/// </summary>
/// <remarks>
/// <exception cref="UserNotExistsException"
/// </remarks>
/// <param name="login"></param>
public class GetUserIdByLoginQuery(
    string login
) :
    IRequest<Guid>
{
    public string Login { get; private init; } = login;
}
