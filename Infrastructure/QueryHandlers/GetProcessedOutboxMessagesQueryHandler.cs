using Dapper;
using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Interfaces.QueryHandlers;
using Infrastructure.Contracts.Queries;


namespace Infrastructure.QueryHandlers;

public class GetProcessedOutboxMessagesQueryHandler(
    ISqlConnectionManager connectionManager
) :
    IGetProcessedOutboxMessagesQueryHandler
{
    private readonly ISqlConnectionManager _connectionManager = connectionManager;

    public async Task<IEnumerable<Guid>> Handle(GetProcessedOutboxMessagesQuery request, CancellationToken cancellationToken)
    {
        var messageIds = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var messageIds = await connection
                        .QueryAsync<Guid>(
                            new CommandDefinition(
                                "[outbox].[GetProcessedMessages]",
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    return messageIds;
                },
                cancellationToken: cancellationToken
            );

        return messageIds;
    }
}
