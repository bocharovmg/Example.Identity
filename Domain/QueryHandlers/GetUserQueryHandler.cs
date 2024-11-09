using AutoMapper;
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
        var getVerificationStatesRequest = new InfrastructureQueries.GetVerificationStatesQuery(userResponse.UserId);

        var verificationStatesResponse = await _mediator.Send(getVerificationStatesRequest, cancellationToken);

        var verificationStates = _mapper.Map<IEnumerable<VerificationStateDto>>(verificationStatesResponse);
        #endregion

        var user = _mapper.Map<UserDto>(userResponse);

        user.VerificationState = verificationStates.First();

        return user;
    }
}
