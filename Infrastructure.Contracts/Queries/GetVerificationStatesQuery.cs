using MediatR;
using Infrastructure.Contracts.Dtos;


namespace Infrastructure.Contracts.Queries;

public class GetVerificationStatesQuery(
    Guid userId
) :
    IRequest<IEnumerable<VerificationStateDto>>
{
    public Guid UserId { get; private init; } = userId;
}
