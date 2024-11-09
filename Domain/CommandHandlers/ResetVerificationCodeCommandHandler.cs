using MediatR;
using AutoMapper;
using DomainCommands = Domain.Contracts.Commands;
using DomainNotifications = Domain.Contracts.Notifications;
using DomainEnums = Domain.Contracts.Enums;
using Domain.Contracts.Interfaces.CommandHandlers;
using Domain.Contracts.Dtos;
using InfrastructureCommands = Infrastructure.Contracts.Commands;
using InfrastructureQueries = Infrastructure.Contracts.Queries;
using InfrastructureEnums = Infrastructure.Contracts.Enums;
using Domain.Contracts.Exceptions;


namespace Domain.CommandHandlers;

public class ResetVerificationCodeCommandHandler : IResetVerificationCodeCommandHandler
{
    private readonly IMediator _mediator;

    public ResetVerificationCodeCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<VerificationStateDto> Handle(DomainCommands.ResetVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var verificationField = (InfrastructureEnums.User.VerificationFieldType)request.VerificationField;

        #region get user id by email
        var getUserIdRequest = new InfrastructureQueries.GetUserIdByLoginQuery(request.Email);

        var userId = await _mediator.Send(getUserIdRequest, cancellationToken);
        #endregion

        await EnsureCanResetVerificationCodeAsync(userId, verificationField, cancellationToken);

        #region generate verification code
        var tryGenerateVerificationCodeRequest = new InfrastructureCommands
            .TryGenerateVerificationCodeCommand(userId, verificationField);

        var verificationCodeDto = await _mediator.Send(tryGenerateVerificationCodeRequest, cancellationToken);
        #endregion

        if (!string.IsNullOrWhiteSpace(verificationCodeDto.VerificationCode))
        {
            #region publish notification: verification code changed
            var verificationCodeCreatedNotification = new DomainNotifications
                .VerificationCodeCreatedNotification(request.Email, verificationCodeDto.VerificationCode, request.VerificationField);

            await _mediator.Publish(verificationCodeCreatedNotification, cancellationToken);
            #endregion
        }

        return new VerificationStateDto
        {
            Countdown = verificationCodeDto.Lifetime,
            VerificationState = (DomainEnums.User.VerificationStateType)verificationCodeDto.VerificationState
        };
    }

    private async Task EnsureCanResetVerificationCodeAsync(
        Guid userId,
        InfrastructureEnums.User.VerificationFieldType verificationField,
        CancellationToken cancellationToken
    )
    {
        var requredVerificationState = RequredVerificationState(verificationField);

        if (requredVerificationState.HasValue)
        {
            #region get verification state
            var getVerificationStatesRequest = new InfrastructureQueries.GetVerificationStatesQuery(userId);

            var verificationStatesResponse = await _mediator.Send(getVerificationStatesRequest, cancellationToken);
            #endregion

            if (!verificationStatesResponse.Any(verificationState => verificationState.VerificationState == requredVerificationState.Value))
            {
                throw new UserIsVerifiedException($"User {verificationField} is verified");
            }
        }
    }

    private InfrastructureEnums.User.VerificationStateType? RequredVerificationState(
        InfrastructureEnums.User.VerificationFieldType verificationField
    )
    {
        switch (verificationField)
        {
            case InfrastructureEnums.User.VerificationFieldType.Email:
                return InfrastructureEnums.User.VerificationStateType.Email;

            case InfrastructureEnums.User.VerificationFieldType.AlternativeEmail:
                return InfrastructureEnums.User.VerificationStateType.AlternativeEmail;

            default:
                return null;
        }
    }
}
