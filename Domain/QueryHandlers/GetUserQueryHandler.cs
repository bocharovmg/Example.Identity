﻿using AutoMapper;
using Domain.Contracts.Dtos;
using Domain.Contracts.Interfaces.QueryHandlers;
using DomainQueries = Domain.Contracts.Queries;
using InfrastructureQueries = Infrastructure.Contracts.Queries;
using MediatR;


namespace Domain.QueryHandlers;

public class GetUserQueryHandler : IGetUserQueryHandler
{
    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    public GetUserQueryHandler(
        IMediator mediator,
        IMapper mapper
    )
    {
        _mediator = mediator;

        _mapper = mapper;
    }

    public async Task<UserDto> Handle(DomainQueries.GetUserQuery request, CancellationToken cancellationToken)
    {
        #region get user
        var getUserRequest = new InfrastructureQueries.GetUserQuery(request.UserId);

        var userResponse = await _mediator.Send(getUserRequest, cancellationToken);
        #endregion

        #region get verification state
        var getVerificationStateRequest = new DomainQueries.GetVerificationStateQuery(userResponse.Email);

        var verificationState = await _mediator.Send(getVerificationStateRequest, cancellationToken);
        #endregion

        var user = _mapper.Map<UserDto>(userResponse);

        user.VerificationState = verificationState;

        return user;
    }
}
