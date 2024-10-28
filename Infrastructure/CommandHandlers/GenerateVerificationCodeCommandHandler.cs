using Dapper;
using Exemple.Identity.Abstractions.Core.Extensions;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using Exemple.Identity.Infrastructure.Contracts.Commands;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;


namespace Exemple.Identity.Infrastructure.CommandHandlers;

public class GenerateVerificationCodeCommandHandler : IGenerateVerificationCodeCommandHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public GenerateVerificationCodeCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<string> Handle(GenerateVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var verificationCode = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var verificationCode = await connection
                        .QuerySingleAsync<string>(
                            new CommandDefinition(
                                "[user].[GenerateVerificationCode]",
                                request,
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

        return verificationCode;
    }
}
