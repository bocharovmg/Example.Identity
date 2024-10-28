using Dapper;
using Exemple.Identity.Abstractions.Core.Extensions;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using Exemple.Identity.Infrastructure.Contracts.Exceptions;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;
using Exemple.Identity.Infrastructure.Contracts.Queries;


namespace Exemple.Identity.Infrastructure.CommandHandlers;

public class GetUserIdByVerificationCodeQueryHandler : IGetUserIdByVerificationCodeQueryHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public GetUserIdByVerificationCodeQueryHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<Guid> Handle(GetUserIdByVerificationCodeQuery request, CancellationToken cancellationToken)
    {
        var userId = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var userId = await connection
                        .QuerySingleOrDefaultAsync<Guid?>(
                            new CommandDefinition(
                                "[user].[GetUserIdByVerificationCode]",
                                request,
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    if (!userId.HasValue)
                    {
                        throw new VerificationCodeNotExistsException("Verification code is not exists");
                    }

                    return userId.Value;
                },
                cancellationToken: cancellationToken
            );

        return userId;
    }
}
