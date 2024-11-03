using MediatR;
using AutoMapper;
using DomainCommands = Domain.Contracts.Commands;
using DomainNotifications = Domain.Contracts.Notifications;
using DomainEnums = Domain.Contracts.Enums;
using Domain.Contracts.Interfaces.CommandHandlers;
using Infrastructure.Contracts.Interfaces.Services;
using Domain.Contracts.Dtos;
using InfrastructureCommands = Infrastructure.Contracts.Commands;
using InfrastructureQueries = Infrastructure.Contracts.Queries;
using InfrastructureEnums = Infrastructure.Contracts.Enums;
using Domain.Extension;


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

        var verificationStateType = verificationField.ToVerificationStateType();

        #region get user id by email
        var getUserIdRequest = new InfrastructureQueries.GetUserIdByLoginQuery(request.Email);

        var userId = await _mediator.Send(getUserIdRequest, cancellationToken);
        #endregion

        #region try get verification state lifetime from the cache
        var lifetime = await _verificationStateLifetimeService.GetLifetimeAsync(userId, verificationStateType, cancellationToken);
        #endregion

        if (!lifetime.HasValue)
        {
            #region generate verification code
            var generateVerificationCodeRequest = new InfrastructureCommands
                .GenerateVerificationCodeCommand(userId, verificationField);

            var verificationCode = await _mediator.Send(generateVerificationCodeRequest, cancellationToken);
            #endregion

            #region save new verification state lifetime to the cache
            lifetime = 120;

            await _verificationStateLifetimeService.AddAsync(userId, verificationStateType, lifetime.Value, cancellationToken);
            #endregion

            #region publish notification: verification code changed
            var verificationCodeCreatedNotification = new DomainNotifications
                .VerificationCodeCreatedNotification(request.Email, verificationCode, request.VerificationField);

            await _mediator.Publish(verificationCodeCreatedNotification, cancellationToken);
            #endregion
        }

        return new VerificationStateDto
        {
            Countdown = lifetime,
            VerificationState = (DomainEnums.User.VerificationStateType)verificationStateType
        };
    }
}
