using Exemple.Identity.Infrastructure.Contracts.Enums.User;
using Exemple.Identity.Infrastructure.Contracts.Queries;
using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IGetVerificationStateTypeQueryHandler : IRequestHandler<GetVerificationStateTypeQuery, VerificationStateType>;
