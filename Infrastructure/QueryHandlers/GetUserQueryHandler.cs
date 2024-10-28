using Exemple.Identity.Abstractions.Core.Extensions;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using Exemple.Identity.Infrastructure.Contracts.Dtos;
using Exemple.Identity.Infrastructure.Contracts.Exceptions;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;
using Exemple.Identity.Infrastructure.Contracts.Queries;
using Dapper;


namespace Exemple.Identity.Infrastructure.CommandHandlers;

public class GetUserQueryHandler : IGetUserQueryHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public GetUserQueryHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var user = await connection
                        .QuerySingleOrDefaultAsync<UserDto?>(
                            new CommandDefinition(
                                "[user].[GetUser]",
                                request,
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    if (user == null)
                    {
                        throw new UserNotExistsException("User is not exists");
                    }

                    return user;
                },
                cancellationToken: cancellationToken
            );

        return user;
    }
}
