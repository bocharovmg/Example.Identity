using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Commands;
using Infrastructure.Contracts.Interfaces.QueryHandlers;
using Dapper;


namespace Infrastructure.CommandHandlers;

public class ConfirmVerificationCodeCommandHandler : IConfirmVerificationCodeCommandHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public ConfirmVerificationCodeCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<bool> Handle(ConfirmVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var result = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var rows = await connection
                        .ExecuteAsync(
                            new CommandDefinition(
                                "[user].[ConfirmVerificationCode]",
                                request,
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    return rows > 0;
                },
                runTransaction: true,
                cancellationToken: cancellationToken
            );

        return result;
    }
}
