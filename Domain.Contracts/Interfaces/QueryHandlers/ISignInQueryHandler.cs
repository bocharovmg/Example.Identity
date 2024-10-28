using Exemple.Identity.Domain.Contracts.Dtos;
using Exemple.Identity.Domain.Contracts.Queries;
using MediatR;


namespace Exemple.Identity.Domain.Contracts.Interfaces.QueryHandlers;

public interface ISignInQueryHandler : IRequestHandler<SignInQuery, AuthDto>;
