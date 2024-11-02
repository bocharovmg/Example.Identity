using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Enums.User;
using Infrastructure.Contracts.Interfaces.QueryHandlers;
using Infrastructure.Contracts.Queries;
using Dapper;


namespace Infrastructure.CommandHandlers;

public class GetVerificationStateTypeQueryHandler : IGetVerificationStateTypeQueryHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public GetVerificationStateTypeQueryHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<VerificationStateType> Handle(GetVerificationStateTypeQuery request, CancellationToken cancellationToken)
    {
        var verificationState = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var verificationState = await connection
                        .QueryFirstOrDefaultAsync<VerificationStateType?>(
                            new CommandDefinition(
                                "[user].[GetVerificationState]",
                                request,
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    return verificationState ?? VerificationStateType.None;
                },
                cancellationToken: cancellationToken
            );

        return verificationState;
    }
}
