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

        #region get user id by email
        var getUserIdRequest = new GetUserIdByLoginQuery(request.Login);

        var userId = await _mediator.Send(getUserIdRequest, cancellationToken);
        #endregion

        #region try get verification state lifetime from the cache
        var lifetime = await _verificationStateLifetimeService.GetLifetimeAsync(userId, verificationState, cancellationToken);
        #endregion

        if (!lifetime.HasValue)
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
                                        UserId = userId,
                                        VerificationField = request.VerificationField
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

            await _verificationStateLifetimeService.AddAsync(userId, verificationState, lifetime.Value, cancellationToken);
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
