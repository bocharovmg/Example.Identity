using MediatR;
using AutoMapper;
using DomainCommands = Domain.Contracts.Commands;
using DomainNotifications = Domain.Contracts.Notifications;
using DomainEnums = Domain.Contracts.Enums;
using Domain.Contracts.Interfaces.CommandHandlers;
using Infrastructure.Contracts.Interfaces.Services;
using Domain.Contracts.Dtos;
using InfrastructureCommands = Infrastructure.Contracts.Commands;
using InfrastructureEnums = Infrastructure.Contracts.Enums;


namespace Domain.CommandHandlers;

public class CreateVerificationCodeCommandHandler : ICreateVerificationCodeCommandHandler
{
    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    private readonly IVerificationStateLifetimeService _verificationStateLifetimeService;

    public CreateVerificationCodeCommandHandler(
        IMediator mediator,
        IMapper mapper,
        IVerificationStateLifetimeService verificationStateLifetimeService
    )
    {
        _mediator = mediator;

        _mapper = mapper;

        _verificationStateLifetimeService = verificationStateLifetimeService;
    }

    public async Task<VerificationStateDto> Handle(DomainCommands.CreateVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var verificationField = (InfrastructureEnums.User.VerificationFieldType)request.VerificationField;

        #region generate verification code
        var tryGenerateVerificationCodeRequest = new InfrastructureCommands
            .TryGenerateVerificationCodeCommand(request.Email, verificationField);

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
}
