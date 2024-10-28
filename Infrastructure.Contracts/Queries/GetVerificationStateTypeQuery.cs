using Exemple.Identity.Infrastructure.Contracts.Enums.User;
using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Queries;

public class GetVerificationStateTypeQuery(
    Guid userId
) :
    IRequest<VerificationStateType>
{
    public Guid UserId { get; private init; } = userId;
}
