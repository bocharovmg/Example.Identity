using Exemple.Identity.Infrastructure.Contracts.Dtos;
using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Queries;

public class GetUserQuery(
    Guid userId
)
    : IRequest<UserDto>
{
    public Guid UserId { get; private init; } = userId;
}
