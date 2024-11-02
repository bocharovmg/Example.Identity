using Abstractions.Core.Extensions;
using Abstractions.Infrastructure.ConnectionManager;
using Infrastructure.Contracts.Commands;
using Infrastructure.Contracts.Exceptions;
using Infrastructure.Contracts.Interfaces.QueryHandlers;
using Dapper;
using Microsoft.Data.SqlClient;


namespace Infrastructure.CommandHandlers;

public class CreateUserCommandHandler : ICreateUserCommandHandler
{
    private readonly ISqlConnectionManager _connectionManager;

    public CreateUserCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = await _connectionManager
            .ExecuteAsync(
                async (connection) =>
                {
                    try
                    {
                        var userId = await connection
                            .QuerySingleAsync<Guid>(
                                new CommandDefinition(
                                    "[user].[CreateUser]",
                                    request,
                                    transaction: _connectionManager.Transaction,
                                    commandType: System.Data.CommandType.StoredProcedure,
                                    cancellationToken: cancellationToken
                                )
                            );

                        return userId;
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.Contains("UIX__auth__UserAttributes_UserAttributeSectionId_Value"))
                        {
                            throw new DuplicateUserException("User with same name or email is exists");
                        }

                        if (ex.Message.Contains("Duplicate alternative email"))
                        {
                            throw new DuplicateUserException("User with same alternative email is exists");
                        }

                        throw;
                    }
                },
                runTransaction: true,
                cancellationToken: cancellationToken
            );

        return userId;
    }
}
