namespace Infrastructure.Contracts;

public class UserContext
{
    public Guid UserId { get; private init; } = Guid.Empty;

    public string Email { get; private init; } = string.Empty;

    public string SecurityToken { get; private init; } = string.Empty;

    public bool IsAuthorized => UserId == Guid.Empty;

    public UserContext()
    { }

    public UserContext(Guid userId, string email, string securityToken)
    {
        UserId = userId;

        Email = email;

        SecurityToken = securityToken;
    }
}
