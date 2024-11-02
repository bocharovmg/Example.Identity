using Domain.Contracts.Dtos;
using Domain.Contracts.Queries;
using MediatR;


namespace Domain.Contracts.Interfaces.QueryHandlers;

public interface IGetSecurityTokenQueryHandler : IRequestHandler<GetSecurityTokenQuery, SecurityTokenDto>;
