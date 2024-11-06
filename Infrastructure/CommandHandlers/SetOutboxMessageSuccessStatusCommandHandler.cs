using Dapper;
using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Commands;
using Infrastructure.Contracts.Interfaces.CommandHandlers;


namespace Infrastructure.CommandHandlers;

public class SetOutboxMessageSuccessStatusCommandHandler : ISetOutboxMessageSuccessStatusCommandHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public SetOutboxMessageSuccessStatusCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task Handle(SetOutboxMessageSuccessStatusCommand request, CancellationToken cancellationToken)
    {
        await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    await connection
                        .ExecuteAsync(
                            new CommandDefinition(
                                "[outbox].[SetMessageSuccessStatus]",
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
