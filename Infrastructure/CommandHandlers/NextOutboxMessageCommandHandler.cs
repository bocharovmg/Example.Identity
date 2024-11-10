using Dapper;
using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Commands;
using Infrastructure.Contracts.Interfaces.CommandHandlers;


namespace Infrastructure.CommandHandlers;

public class NextOutboxMessageCommandHandler : INextOutboxMessageCommandHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public NextOutboxMessageCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<OutboxMessageDto?> Handle(NextOutboxMessageCommand request, CancellationToken cancellationToken)
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
                runTransaction: true,
                cancellationToken: cancellationToken
            );

        return outboxMessage;
    }
}
