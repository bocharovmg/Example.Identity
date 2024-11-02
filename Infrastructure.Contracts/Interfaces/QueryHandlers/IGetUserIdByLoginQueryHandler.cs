using Infrastructure.Contracts.Queries;
using MediatR;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IGetUserIdByLoginQueryHandler : IRequestHandler<GetUserIdByLoginQuery, Guid>;
