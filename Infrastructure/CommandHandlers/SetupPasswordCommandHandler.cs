﻿using Exemple.Identity.Abstractions.Core.Extensions;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using Exemple.Identity.Infrastructure.Contracts.Commands;
using Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;
using Dapper;


namespace Exemple.Identity.Infrastructure.CommandHandlers;

public class SetupPasswordCommandHandler : ISetupPasswordCommandHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public SetupPasswordCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<bool> Handle(SetupPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    var rows = await connection
                        .ExecuteAsync(
                            new CommandDefinition(
                                "[user].[SetupPassword]",
                                request,
                                transaction: _connectionManager.Transaction,
                                commandType: System.Data.CommandType.StoredProcedure,
                                cancellationToken: cancellationToken
                            )
                        );

                    return rows > 0;
                },
                runTransaction: true,
                cancellationToken: cancellationToken
            );

        return result;
    }
}