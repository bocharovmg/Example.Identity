using Exemple.Identity.Infrastructure.Contracts.Queries;
using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IAuthQueryHandler : IRequestHandler<AuthQuery, Guid>;
