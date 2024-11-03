using AutoMapper;
using MediatR;
using DomainCommands = Domain.Contracts.Commands;
using DomainQueries = Domain.Contracts.Queries;
using DomainNotifications = Domain.Contracts.Notifications;
using Domain.Contracts.Dtos;
using Domain.Contracts.Interfaces.CommandHandlers;
using InfrastructureCommands = Infrastructure.Contracts.Commands;
using Domain.Contracts.Enums.User;


namespace Domain.CommandHandlers;

public class SignUpCommandHandler : ISignUpCommandHandler
{
    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    public SignUpCommandHandler(
        IMediator mediator,
        IMapper mapper
    )
    {
        _mediator = mediator;

        _mapper = mapper;
    }

    public async Task<AuthDto> Handle(DomainCommands.SignUpCommand request, CancellationToken cancellationToken)
    {
        #region create user
        var createUserRequest = _mapper.Map<InfrastructureCommands.CreateUserCommand>(request);

        var userId = await _mediator.Send(createUserRequest, cancellationToken);
        #endregion

        #region publish notification: user created
        var userCreatedNotification = new DomainNotifications
            .UserCreatedNotification(userId, request.Name, request.Email, request.AlternativeEmail);

        await _mediator.Publish(userCreatedNotification);
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
