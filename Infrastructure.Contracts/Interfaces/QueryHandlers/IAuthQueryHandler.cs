using Infrastructure.Contracts.Queries;
using MediatR;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IAuthQueryHandler : IRequestHandler<AuthQuery, Guid>;
