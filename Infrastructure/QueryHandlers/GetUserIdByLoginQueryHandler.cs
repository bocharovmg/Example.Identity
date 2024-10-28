using Exemple.Identity.Abstractions.Core.Extensions;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using Exemple.Identity.Infrastructure.Contracts.Exceptions;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;
using Exemple.Identity.Infrastructure.Contracts.Queries;
using Dapper;


namespace Exemple.Identity.Infrastructure.CommandHandlers;

public class GetUserIdByLoginQueryHandler : IGetUserIdByLoginQueryHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public GetUserIdByLoginQueryHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<Guid> Handle(GetUserIdByLoginQuery request, CancellationToken cancellationToken)
    {
        var userId = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var userId = await connection
                        .QuerySingleOrDefaultAsync<Guid?>(
                            new CommandDefinition(
                                "[user].[GetUserIdByLogin]",
                                request,
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    if (!userId.HasValue)
                    {
                        throw new UserNotExistsException("Failed to find user by login");
                    }

                    return userId.Value;
                },
                cancellationToken: cancellationToken
            );

        return userId;
    }
}
