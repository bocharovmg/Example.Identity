﻿using MediatR;
using Exemple.Identity.Domain.Contracts.Dtos;
using Exemple.Identity.Domain.Contracts.Interfaces.QueryHandlers;
using DomainQueries = Exemple.Identity.Domain.Contracts.Queries;
using DomainEnums = Exemple.Identity.Domain.Contracts.Enums;
using Exemple.Identity.Domain.Models;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.Services;
using InfrastructureQueries = Exemple.Identity.Infrastructure.Contracts.Queries;
using InfrastructureEnums = Exemple.Identity.Infrastructure.Contracts.Enums;


namespace Exemple.Identity.Domain.QueryHandlers;

public class GetVerificationStateQueryHandler : IGetVerificationStateQueryHandler
{
    private readonly IMediator _mediator;

    private readonly IVerificationStateCache _verificationStateCache;

    public GetVerificationStateQueryHandler(
        IMediator mediator,
        IVerificationStateCache verificationStateCache
    )
    {
        _mediator = mediator;

        _verificationStateCache = verificationStateCache;
    }

    public async Task<VerificationStateDto> Handle(DomainQueries.GetVerificationStateQuery request, CancellationToken cancellationToken)
    {
        #region get user id
        var getUserIdRequest = new InfrastructureQueries
            .GetUserIdByLoginQuery(request.Email);

        var userId = await _mediator.Send(getUserIdRequest, cancellationToken);
        #endregion

        #region get verification state type
        var getVerificationStateRequest = new InfrastructureQueries.GetVerificationStateTypeQuery(userId);

        var verificationStateType = await _mediator.Send(getVerificationStateRequest, cancellationToken);
        #endregion

        #region try get and return existing verification state
        var lifetime = await _verificationStateCache.GetLifetimeAsync(userId, verificationStateType, cancellationToken);
        #endregion

        return new VerificationStateDto
        {
            Countdown = lifetime,
            VerificationState = (DomainEnums.User.VerificationStateType)verificationStateType
        };
    }
}
