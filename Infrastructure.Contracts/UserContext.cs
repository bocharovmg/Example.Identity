namespace Exemple.Identity.Infrastructure.Contracts;

public class UserContext
{
    public Guid UserId { get; private init; }

    public string SecurityToken { get; private init; } = string.Empty;

    public bool IsAuthorized => UserId == Guid.Empty;

    public UserContext()
    { }

    public UserContext(Guid userId, string securityToken)
    {
        UserId = userId;

        SecurityToken = securityToken;
    }
}
