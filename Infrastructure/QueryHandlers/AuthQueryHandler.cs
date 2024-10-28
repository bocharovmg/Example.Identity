using Exemple.Identity.Abstractions.Core.Extensions;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using Exemple.Identity.Infrastructure.Contracts.Exceptions;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;
using Exemple.Identity.Infrastructure.Contracts.Queries;
using Dapper;


namespace Exemple.Identity.Infrastructure.CommandHandlers;

public class AuthQueryHandler : IAuthQueryHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public AuthQueryHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<Guid> Handle(AuthQuery request, CancellationToken cancellationToken)
    {
        var userId = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var userId = await connection
                        .QuerySingleOrDefaultAsync<Guid?>(
                            new CommandDefinition(
                                "[user].[Auth]",
                                request,
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    if (!userId.HasValue)
                    {
                        throw new InvalidLoginOrPasswordException("Invalid login or password");
                    }

                    return userId.Value;
                },
                cancellationToken: cancellationToken
            );

        return userId;
    }
}
