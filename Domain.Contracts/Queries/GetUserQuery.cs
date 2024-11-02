using Domain.Contracts.Dtos;
using MediatR;


namespace Domain.Contracts.Queries;

public class GetUserQuery(
    Guid userId
) :
    IRequest<UserDto>
{
    public Guid UserId { get; set; } = userId;
}
