using Exemple.Identity.Infrastructure.Contracts.Dtos;
using Exemple.Identity.Infrastructure.Contracts.Queries;
using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IGetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>;
