using MediatR;
using Dapper;
using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Commands;
using Infrastructure.Contracts.Interfaces.QueryHandlers;
using Infrastructure.Contracts.Interfaces.Services;
using Infrastructure.Contracts.Queries;
using Infrastructure.Extensions;
using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Enums.User;


namespace Infrastructure.CommandHandlers;

public class TryGenerateVerificationCodeCommandHandler : ITryGenerateVerificationCodeCommandHandler
{
    private readonly IMediator _mediator;

    private readonly ISqlConnectionManager _connectionManager;

    private readonly IVerificationStateLifetimeService _verificationStateLifetimeService;

    public TryGenerateVerificationCodeCommandHandler(
        IMediator mediator,
        ISqlConnectionManager connectionManager,
        IVerificationStateLifetimeService verificationStateLifetimeService
    )
    {
        _mediator = mediator;

        _connectionManager = connectionManager;

        _verificationStateLifetimeService = verificationStateLifetimeService;
    }

    public async Task<VerificationCodeDto> Handle(TryGenerateVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var verificationState = request.VerificationField.ToVerificationStateType();

        var userAttributeSection = request.VerificationField.ToUserAttributeSection();

        #region try get verification state lifetime from the cache
        var lifetime = await _verificationStateLifetimeService.GetLifetimeAsync(request.UserId, verificationState, cancellationToken);
        #endregion

        if (lifetime.GetValueOrDefault() <= 0)
        {
            var verificationCode = await _connectionManager
                .ExecuteAsync(
                    async (connection) =>
                    {
                        var verificationCode = await connection
                            .QuerySingleAsync<string>(
                                new CommandDefinition(
                                    "[user].[GenerateVerificationCode]",
                                    new
                                    {
                                        UserId = request.UserId,
                                        UserAttributeSectionId = userAttributeSection
                                    },
                                    transaction: _connectionManager.Transaction,
                                    commandType: System.Data.CommandType.StoredProcedure,
                                    cancellationToken: cancellationToken
                                )
                            );

                        return verificationCode;
                    },
                    runTransaction: true,
                    cancellationToken: cancellationToken
                );

            #region save new verification state lifetime
            lifetime = 120;

            await _verificationStateLifetimeService.AddAsync(request.UserId, verificationState, lifetime.Value, cancellationToken);
            #endregion

            return new VerificationCodeDto
            {
                VerificationCode = verificationCode,
                Lifetime = lifetime.Value,
                VerificationState = verificationState
            };
        }

        return new VerificationCodeDto
        {
            Lifetime = lifetime.Value,
            VerificationState = verificationState
        };
    }
}
