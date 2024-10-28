using Exemple.Identity.Infrastructure.Contracts.Queries;
using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IGetUserIdByLoginQueryHandler : IRequestHandler<GetUserIdByLoginQuery, Guid>;
