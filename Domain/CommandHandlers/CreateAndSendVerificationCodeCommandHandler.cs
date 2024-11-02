using MediatR;
using AutoMapper;
using DomainCommands = Domain.Contracts.Commands;
using DomainEnums = Domain.Contracts.Enums;
using Domain.Contracts.Interfaces.CommandHandlers;
using Infrastructure.Contracts.Interfaces.Services;
using Domain.Contracts.Dtos;
using InfrastructureCommands = Infrastructure.Contracts.Commands;
using InfrastructureQueries = Infrastructure.Contracts.Queries;
using InfrastructureNotifications = Infrastructure.Contracts.Notifications;
using InfrastructureEnums = Infrastructure.Contracts.Enums;
using Domain.Extension;


namespace Domain.CommandHandlers;

public class CreateAndSendVerificationCodeCommandHandler : ICreateAndSendVerificationCodeCommandHandler
{
    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    private readonly IVerificationStateCache _verificationStateCache;

    public CreateAndSendVerificationCodeCommandHandler(
        IMediator mediator,
        IMapper mapper,
        IVerificationStateCache verificationStateCache
    )
    {
        _mediator = mediator;

        _mapper = mapper;

        _verificationStateCache = verificationStateCache;
    }

    public async Task<VerificationStateDto> Handle(DomainCommands.CreateAndSendVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var verificationField = (InfrastructureEnums.User.VerificationFieldType)request.VerificationField;

        var verificationStateType = verificationField.ToVerificationStateType();

        #region get user id by email
        var getUserIdRequest = new InfrastructureQueries.GetUserIdByLoginQuery(request.Email);

        var userId = await _mediator.Send(getUserIdRequest, cancellationToken);
        #endregion

        #region get user
        var getUserRequest = new InfrastructureQueries.GetUserQuery(userId);

        var user = await _mediator.Send(getUserRequest, cancellationToken);
        #endregion

        #region try get verification state lifetime from the cache
        var lifetime = await _verificationStateCache.GetLifetimeAsync(userId, verificationStateType, cancellationToken);
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

            await _verificationStateCache.AddAsync(userId, verificationStateType, lifetime.Value, cancellationToken);
            #endregion

            #region publish notification: verification code changed
            var verificationCodeCreatedNotification = new InfrastructureNotifications
                .VerificationCodeCreatedNotification(user.Email, verificationCode, verificationField);

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
