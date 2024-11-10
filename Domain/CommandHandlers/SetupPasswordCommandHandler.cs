using MediatR;
using Domain.Contracts.Interfaces.CommandHandlers;
using DomainCommands = Domain.Contracts.Commands;
using InfrastructureCommands = Infrastructure.Contracts.Commands;
using InfrastructureQueries = Infrastructure.Contracts.Queries;
using Infrastructure.Contracts.Enums.User;
using Domain.Contracts.Exceptions;


namespace Domain.CommandHandlers;

public class SetupPasswordCommandHandler : ISetupPasswordCommandHandler
{
    private readonly IMediator _mediator;

    public SetupPasswordCommandHandler(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    public async Task Handle(DomainCommands.SetupPasswordCommand request, CancellationToken cancellationToken)
    {
        #region get user id
        var getUserIdRequest = new InfrastructureQueries
            .GetUserIdByLoginQuery(request.Email);

        var userId = await _mediator.Send(getUserIdRequest, cancellationToken);
        #endregion

        await EnsureSetupPasswordVerificationStateAsync(userId, cancellationToken);

        #region confirm verification code
        var confirmVerificationCodeRequest = new InfrastructureCommands
            .ConfirmVerificationCodeCommand(
                userId,
                request.VerificationCode,
                VerificationFieldType.Password
            );

        if (!await _mediator.Send(confirmVerificationCodeRequest, cancellationToken))
        {
            throw new WrongVerificationCodeException("Wrong verification code");
        }
        #endregion

        #region setup password
        var setupPasswordRequest = new InfrastructureCommands
            .SetupPasswordCommand(userId, request.Password);

        if (!await _mediator.Send(setupPasswordRequest, cancellationToken))
        {
            throw new BaseDomainException("Failed to setup password");
        }
        #endregion
    }

    private async Task EnsureSetupPasswordVerificationStateAsync(Guid userId, CancellationToken cancellationToken)
    {
        var passwordVerificationState = await GetPasswordVerificationStateAsync(userId, cancellationToken);

        if (passwordVerificationState == null)
        {
            throw new VerificationIsNotStartedException("The reset password process is not started");
        }

        if (!passwordVerificationState.Lifetime.HasValue)
        {
            throw new VerificationCodeIsExpiredException("The verification code is expired");
        }
    }

    private async Task<Infrastructure.Contracts.Dtos.VerificationStateDto?> GetPasswordVerificationStateAsync(Guid userId, CancellationToken cancellationToken)
    {
        #region get verification state
        var getVerificationStatesRequest = new InfrastructureQueries.GetVerificationStatesQuery(userId);

        var getVerificationStatesResponse = await _mediator.Send(getVerificationStatesRequest, cancellationToken);
        #endregion

        return getVerificationStatesResponse
            .FirstOrDefault(verificationState =>
                verificationState.VerificationState == VerificationStateType.Password
            );
    }
}
