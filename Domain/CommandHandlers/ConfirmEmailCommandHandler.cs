using MediatR;
using AutoMapper;
using DomainCommands = Domain.Contracts.Commands;
using Domain.Contracts.Interfaces.CommandHandlers;
using InfrastructureCommands = Infrastructure.Contracts.Commands;
using InfrastructureQueries = Infrastructure.Contracts.Queries;
using Infrastructure.Contracts.Enums.User;
using Domain.Contracts.Exceptions;


namespace Domain.CommandHandlers;

public class ConfirmEmailCommandHandler : IConfirmEmailCommandHandler
{
    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    public ConfirmEmailCommandHandler(
        IMediator mediator,
        IMapper mapper
    )
    {
        _mediator = mediator;

        _mapper = mapper;
    }

    public async Task Handle(DomainCommands.ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var verificationField = !request.IsAlternativeEmail
            ? VerificationFieldType.Email
            : VerificationFieldType.AlternativeEmail;

        #region get user id by email
        var getUserIdRequest = new InfrastructureQueries.GetUserIdByLoginQuery(request.Email);

        var userId = await _mediator.Send(getUserIdRequest, cancellationToken);
        #endregion

        await EnsureConfirmEmailVerificationStateAsync(userId, request.IsAlternativeEmail, cancellationToken);

        #region confirm verification code
        var confirmVerificationCodeRequest = new InfrastructureCommands
            .ConfirmVerificationCodeCommand(
                userId,
                request.VerificationCode,
                verificationField
            );

        if (!await _mediator.Send(confirmVerificationCodeRequest, cancellationToken))
        {
            throw new WrongVerificationCodeException("Wrong verification code");
        }
        #endregion
    }

    private async Task EnsureConfirmEmailVerificationStateAsync(Guid userId, bool isAlternativeEmail, CancellationToken cancellationToken)
    {
        var emailVerificationState = await GetEmailVerificationStateAsync(userId, isAlternativeEmail, cancellationToken);

        if (emailVerificationState == null)
        {
            throw new VerificationIsNotStartedException($"The {(isAlternativeEmail ? "alternative email" : "email")} verification is not started");
        }

        if (!emailVerificationState.Lifetime.HasValue)
        {
            throw new VerificationCodeIsExpiredException("The verification code is expired");
        }
    }

    private async Task<Infrastructure.Contracts.Dtos.VerificationStateDto?> GetEmailVerificationStateAsync(Guid userId, bool isAlternativeEmail, CancellationToken cancellationToken)
    {
        #region get verification state
        var getVerificationStatesRequest = new InfrastructureQueries.GetVerificationStatesQuery(userId);

        var getVerificationStatesResponse = await _mediator.Send(getVerificationStatesRequest, cancellationToken);
        #endregion

        return getVerificationStatesResponse
            .FirstOrDefault(verificationState =>
                (
                    !isAlternativeEmail
                    && verificationState.VerificationState == VerificationStateType.Email
                )
                || (
                    isAlternativeEmail
                    && verificationState.VerificationState == VerificationStateType.AlternativeEmail
                )
            );
    }
}
