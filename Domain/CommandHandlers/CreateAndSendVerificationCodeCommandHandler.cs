using MediatR;
using AutoMapper;
using DomainCommands = Exemple.Identity.Domain.Contracts.Commands;
using DomainEnums = Exemple.Identity.Domain.Contracts.Enums;
using Exemple.Identity.Domain.Contracts.Interfaces.CommandHandlers;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.Services;
using Exemple.Identity.Abstractions.Core.Commands;
using Exemple.Identity.Domain.Contracts.Dtos;
using InfrastructureCommands = Exemple.Identity.Infrastructure.Contracts.Commands;
using InfrastructureQueries = Exemple.Identity.Infrastructure.Contracts.Queries;
using InfrastructureNotifications = Exemple.Identity.Infrastructure.Contracts.Notifications;
using InfrastructureEnums = Exemple.Identity.Infrastructure.Contracts.Enums;
using Exemple.Identity.Domain.Extension;


namespace Exemple.Identity.Domain.CommandHandlers;

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

        #region get user id
        var getUserIdRequest = new InfrastructureQueries.GetUserIdByLoginQuery(request.Email);

        var userId = await _mediator.Send(getUserIdRequest, cancellationToken);
        #endregion

        #region get user
        var getUserRequest = new InfrastructureQueries.GetUserQuery(userId);

        var user = await _mediator.Send(getUserRequest, cancellationToken);
        #endregion

        #region try get and return existing verification state
        var lifetime = await _verificationStateCache.GetLifetimeAsync(userId, verificationStateType, cancellationToken);

        if (lifetime.GetValueOrDefault() > 0)
        {
            return new VerificationStateDto
            {
                Countdown = lifetime,
                VerificationState = (DomainEnums.User.VerificationStateType)verificationStateType
            };
        }
        #endregion

        try
        {
            await _mediator.Send(new BeginChangesCommand(), cancellationToken);

            #region generate verification code
            var generateVerificationCodeRequest = new InfrastructureCommands
                .GenerateVerificationCodeCommand(userId, verificationField);

            var verificationCode = await _mediator.Send(generateVerificationCodeRequest, cancellationToken);
            #endregion

            #region create new verification state
            lifetime = 120;

            await _verificationStateCache.AddAsync(userId, verificationStateType, lifetime.Value, cancellationToken);
            #endregion

            await _mediator.Send(new ApplyChangesCommand(), cancellationToken);

            #region send verification code
            var sendVerificationCodeRequest = new InfrastructureNotifications
                .VerificationCodeCreatedNotification(user.Email, verificationCode, verificationField);

            await _mediator.Publish(sendVerificationCodeRequest, cancellationToken);
            #endregion

            return new VerificationStateDto
            {
                Countdown = lifetime,
                VerificationState = (DomainEnums.User.VerificationStateType)verificationStateType
            };
        }
        catch
        {
            await _mediator.Send(new DiscardChangesCommand(), cancellationToken);

            throw;
        }
    }
}
