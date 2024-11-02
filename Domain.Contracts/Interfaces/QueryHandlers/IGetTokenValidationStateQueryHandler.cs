using Domain.Contracts.Enums.Jwt;
using Domain.Contracts.Queries;
using MediatR;


namespace Domain.Contracts.Interfaces.QueryHandlers;

public interface IGetTokenValidationStateQueryHandler : IRequestHandler<GetTokenValidationStateQuery, TokenValidationState>;
