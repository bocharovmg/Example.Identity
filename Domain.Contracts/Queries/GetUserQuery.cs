using Exemple.Identity.Domain.Contracts.Dtos;
using MediatR;


namespace Exemple.Identity.Domain.Contracts.Queries;

public class GetUserQuery(
    Guid userId
) :
    IRequest<UserDto>
{
    public Guid UserId { get; set; } = userId;
}
