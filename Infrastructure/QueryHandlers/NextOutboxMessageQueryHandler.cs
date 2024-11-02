using Dapper;
using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Interfaces.QueryHandlers;
using Infrastructure.Contracts.Queries;


namespace Infrastructure.QueryHandlers;

public class NextOutboxMessageQueryHandler : INextOutboxMessageQueryHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public NextOutboxMessageQueryHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<OutboxMessageDto?> Handle(NextOutboxMessageQuery request, CancellationToken cancellationToken)
    {
        var outboxMessage = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var outboxMessage = await connection
                        .QuerySingleOrDefaultAsync<OutboxMessageDto?>(
                            new CommandDefinition(
                                "[outbox].[NextMessage]",
                                request,
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    return outboxMessage;
                },
                cancellationToken: cancellationToken
            );

        return outboxMessage;
    }
}
