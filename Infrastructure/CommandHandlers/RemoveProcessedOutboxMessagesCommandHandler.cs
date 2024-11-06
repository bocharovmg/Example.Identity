using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Dapper;
using Infrastructure.Contracts.Commands;
using Infrastructure.Contracts.Interfaces.CommandHandlers;
using Infrastructure.Extensions;


namespace Infrastructure.CommandHandlers;

public class RemoveProcessedOutboxMessagesCommandHandler(
    ISqlConnectionManager connectionManager
) :
    IRemoveProcessedOutboxMessagesCommandHandler
{
    private readonly ISqlConnectionManager _connectionManager = connectionManager;

    public async Task Handle(RemoveProcessedOutboxMessagesCommand request, CancellationToken cancellationToken)
    {
        await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var messageIdsDataTable = request.MessageIds.AsDataTable("Id");

                    var messageIds = await connection
                        .ExecuteAsync(
                            new CommandDefinition(
                                "[outbox].[RemoveProcessedMessages]",
                                new
                                {
                                    MessageIds = messageIdsDataTable.AsTableValuedParameter("[outbox].[UT_GuidIdentifier]"),
                                },
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    return messageIds;
                },
                cancellationToken: cancellationToken,
                runTransaction: true
            );
    }
}
