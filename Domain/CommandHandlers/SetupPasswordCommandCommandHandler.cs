using MediatR;
using Domain.Contracts.Interfaces.CommandHandlers;
using DomainCommands = Domain.Contracts.Commands;
using InfrastructureCommands = Infrastructure.Contracts.Commands;
using InfrastructureQueries = Infrastructure.Contracts.Queries;


namespace Domain.CommandHandlers;

public class SetupPasswordCommandCommandHandler : ISetupPasswordCommandCommandHandler
{
    private readonly IMediator _mediator;

    public SetupPasswordCommandCommandHandler(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    public async Task<bool> Handle(DomainCommands.SetupPasswordCommand request, CancellationToken cancellationToken)
    {
        #region get user id
        var getUserIdRequest = new InfrastructureQueries
            .GetUserIdByVerificationCodeQuery(request.VerificationCode);

        var userId = await _mediator.Send(getUserIdRequest, cancellationToken);
        #endregion

        #region confirm verification code
        var confirmVerificationCodeRequest = new DomainCommands
            .ConfirmVerificationCodeCommand(request.VerificationCode);

        if (!await _mediator.Send(confirmVerificationCodeRequest, cancellationToken))
        {
            return false;
        }
        #endregion

        #region setup password
        var setupPasswordRequest = new InfrastructureCommands
            .SetupPasswordCommand(userId, request.Password);

        if (!await _mediator.Send(setupPasswordRequest, cancellationToken))
        {
            return false;
        }
        #endregion

        return true;
    }
}
