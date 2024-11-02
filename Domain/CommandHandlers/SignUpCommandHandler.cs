using AutoMapper;
using MediatR;
using DomainCommands = Domain.Contracts.Commands;
using Domain.Contracts.Dtos;
using Domain.Contracts.Interfaces.CommandHandlers;
using InfrastructureCommands = Infrastructure.Contracts.Commands;
using DomainQueries = Domain.Contracts.Queries;
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

        #region create verification code
        var createVerificationCodeRequest = new DomainCommands.CreateAndSendVerificationCodeCommand(request.Email, VerificationFieldType.Email);

        await _mediator.Send(createVerificationCodeRequest, cancellationToken);
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
