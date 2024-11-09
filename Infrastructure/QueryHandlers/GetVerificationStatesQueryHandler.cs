using Dapper;
using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Enums.User;
using Infrastructure.Contracts.Interfaces.QueryHandlers;
using Infrastructure.Contracts.Queries;
using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Interfaces.Services;


namespace Infrastructure.CommandHandlers;

public class GetVerificationStatesQueryHandler : IGetVerificationStatesQueryHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    private readonly IVerificationStateLifetimeService _verificationStateLifetime;

    public GetVerificationStatesQueryHandler(
        ISqlConnectionManager connectionManager,
        IVerificationStateLifetimeService verificationStateLifetime
    )
    {
        _connectionManager = connectionManager;

        _verificationStateLifetime = verificationStateLifetime;
    }

    public async Task<IEnumerable<VerificationStateDto>> Handle(GetVerificationStatesQuery request, CancellationToken cancellationToken)
    {
        var verificationStates = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var verificationStates = await connection
                        .QueryAsync<VerificationStateType>(
                            new CommandDefinition(
                                "[user].[GetVerificationState]",
                                request,
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    if (!verificationStates.Any())
                    {
                        return [VerificationStateType.None];
                    }

                    return verificationStates;
                },
                cancellationToken: cancellationToken
            );

        return verificationStates
            .Select(verificationState => {
                var lifetimeTask = _verificationStateLifetime.GetLifetimeAsync(request.UserId, verificationState, cancellationToken);

                var lifetime = lifetimeTask.Result;

                return new VerificationStateDto
                {
                    Lifetime = lifetime,
                    VerificationState = verificationState,
                };
            });
    }
}
