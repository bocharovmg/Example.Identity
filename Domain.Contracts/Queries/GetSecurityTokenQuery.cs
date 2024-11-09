using MediatR;
using Domain.Contracts.Dtos;


namespace Domain.Contracts.Queries;

public class GetSecurityTokenQuery(
    Guid userId,
    string email
) :
    IRequest<SecurityTokenDto>
{
    public Guid UserId { get; private init; } = userId;

    public string Email { get; private init; } = email;
}
