using MediatR;
using Exemple.Identity.Domain.Contracts.Interfaces.CommandHandlers;
using DomainCommands = Exemple.Identity.Domain.Contracts.Commands;
using Exemple.Identity.Abstractions.Core.Commands;
using InfrastructureCommands = Exemple.Identity.Infrastructure.Contracts.Commands;
using InfrastructureQueries = Exemple.Identity.Infrastructure.Contracts.Queries;


namespace Exemple.Identity.Domain.CommandHandlers;

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

        await _mediator.Send(new BeginChangesCommand());

        try
        {
            #region confirm verification code
            var confirmVerificationCodeRequest = new DomainCommands
                .ConfirmVerificationCodeCommand(request.VerificationCode);

            if (!await _mediator.Send(confirmVerificationCodeRequest, cancellationToken))
            {
                await _mediator.Send(new DiscardChangesCommand());

                return false;
            }
            #endregion

            #region setup password
            var setupPasswordRequest = new InfrastructureCommands
                .SetupPasswordCommand(userId, request.Password);

            if (!await _mediator.Send(setupPasswordRequest, cancellationToken))
            {
                await _mediator.Send(new DiscardChangesCommand());

                return false;
            }
            #endregion

            await _mediator.Send(new ApplyChangesCommand(), cancellationToken);

            return true;
        }
        catch
        {
            await _mediator.Send(new DiscardChangesCommand());

            throw;
        }
    }
}
