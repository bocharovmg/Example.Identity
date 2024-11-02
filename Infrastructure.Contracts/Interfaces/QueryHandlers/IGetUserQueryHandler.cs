using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Queries;
using MediatR;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IGetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>;
