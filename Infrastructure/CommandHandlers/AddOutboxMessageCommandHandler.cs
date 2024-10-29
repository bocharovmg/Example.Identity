﻿using Dapper;
using Exemple.Identity.Abstractions.Core.Extensions;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Commands;
using Infrastructure.Contracts.Interfaces.CommandHandlers;


namespace Infrastructure.CommandHandlers;

public class AddOutboxMessageCommandHandler : IAddOutboxMessageCommandHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public AddOutboxMessageCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task Handle(AddOutboxMessageCommand request, CancellationToken cancellationToken)
    {
        await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    await connection
                        .ExecuteAsync(
                            new CommandDefinition(
                                "[outbox].[AddMessage]",
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
