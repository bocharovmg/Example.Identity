using Dapper;
using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Commands;
using Infrastructure.Contracts.Interfaces.CommandHandlers;


namespace Infrastructure.CommandHandlers;

public class RemoveOutboxMessageCommandHandler : IRemoveOutboxMessageCommandHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public RemoveOutboxMessageCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task Handle(RemoveOutboxMessageCommand request, CancellationToken cancellationToken)
    {
        await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    await connection
                        .ExecuteAsync(
                            new CommandDefinition(
                                "[outbox].[RemoveMessage]",
                                request,
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );
                },
                runTransaction: true,
                cancellationToken: cancellationToken
            );
    }
}
