using MediatR;
using Domain.Contracts.Dtos;
using Domain.Contracts.Queries;


namespace Domain.Contracts.Interfaces.QueryHandlers;

public interface IGetSecurityTokenStateQueryHandler : IRequestHandler<GetSecurityTokenStateQuery, SecurityTokenStateDto>;
