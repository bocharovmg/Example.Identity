using DomainQueries = Exemple.Identity.Domain.Contracts.Queries;
using InfrastructureQueries = Exemple.Identity.Infrastructure.Contracts.Queries;
using MediatR;
using Exemple.Identity.Domain.Contracts.Dtos;
using Exemple.Identity.Domain.Contracts.Interfaces.QueryHandlers;
using AutoMapper;


namespace Exemple.Identity.Domain.QueryHandlers;

public class SignInQueryHandler : ISignInQueryHandler
{
    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    public SignInQueryHandler(
        IMediator mediator,
        IMapper mapper
    )
    {
        _mediator = mediator;

        _mapper = mapper;
    }

    public async Task<AuthDto> Handle(DomainQueries.SignInQuery request, CancellationToken cancellationToken)
    {
        #region auth user
        var authRequest = _mapper.Map<InfrastructureQueries.AuthQuery>(request);

        var userId = await _mediator.Send(authRequest, cancellationToken);
        #endregion

        #region get user
        var getUserRequest = new DomainQueries.GetUserQuery(userId);

        var user = await _mediator.Send(getUserRequest, cancellationToken);
        #endregion

        #region get security token
        var getSecurityTokenRequest = _mapper.Map<DomainQueries.GetSecurityTokenQuery>(user);

        var securityToken = await _mediator.Send(getSecurityTokenRequest, cancellationToken);
        #endregion

        return new AuthDto
        {
            User = user,
            SecurityToken = securityToken
        };
    }
}
