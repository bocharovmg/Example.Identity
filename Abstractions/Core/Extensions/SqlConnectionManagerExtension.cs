using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using Microsoft.Data.SqlClient;


namespace Exemple.Identity.Abstractions.Core.Extensions;

/// <summary>
/// Run method with database connection.
/// 
/// Fail will automatically rollback transaction and close connection
/// </summary>
public static class SqlConnectionManagerExtension
{
    public static async Task<T> ExecuteAsync<T>(
        this ISqlConnectionManager connectionManager,
        Func<SqlConnection, Task<T>> handlerFunc,
        bool runTransaction = false,
        CancellationToken cancellationToken = default
    )
    {
        var connection = await connectionManager.OpenConnectionAsync();

        try
        {
            if (runTransaction)
            {
                await connectionManager.BeginChangesAsync(cancellationToken: cancellationToken);
            }

            var result = await handlerFunc(connection);

            if (runTransaction)
            {
                await connectionManager.ApplyChangesAsync(cancellationToken);
            }

            await connectionManager.CloseConnectionAsync(cancellationToken: cancellationToken);

            return result;
        }
        catch
        {
            await connectionManager.CloseConnectionAsync(true, cancellationToken);

            throw;
        }
    }

    /// <summary>
    /// Run method with database connection.
    /// 
    /// Fail will automatically rollback transaction and close connection
    /// </summary>
    public static async Task ExecuteAsync(
        this ISqlConnectionManager connectionManager,
        Func<SqlConnection, Task> handlerFunc,
        bool runTransaction = false,
        CancellationToken cancellationToken = default
    )
    {
        var connection = await connectionManager.OpenConnectionAsync();

        try
        {
            if (runTransaction)
            {
                await connectionManager.BeginChangesAsync(cancellationToken: cancellationToken);
            }

            await handlerFunc(connection);

            if (runTransaction)
            {
                await connectionManager.ApplyChangesAsync(cancellationToken);
            }

            await connectionManager.CloseConnectionAsync(cancellationToken: cancellationToken);
        }
        catch
        {
            await connectionManager.CloseConnectionAsync(true, cancellationToken);

            throw;
        }
    }
}
