using Exemple.Identity.Domain.Contracts.Enums.Jwt;
using Exemple.Identity.Domain.Contracts.Queries;
using MediatR;


namespace Exemple.Identity.Domain.Contracts.Interfaces.QueryHandlers;

public interface IGetTokenValidationStateQueryHandler : IRequestHandler<GetTokenValidationStateQuery, TokenValidationState>;
