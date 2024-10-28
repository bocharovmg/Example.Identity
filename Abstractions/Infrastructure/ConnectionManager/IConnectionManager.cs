namespace Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;

public interface IConnectionManager<TConnection, TTransaction>
{
    bool IsTransactionStarted { get; }

    TTransaction? Transaction { get; }

    Task<TConnection> OpenConnectionAsync(CancellationToken cancellationToken = default);

    Task CloseConnectionAsync(bool forceClose = false, CancellationToken cancellationToken = default);

    Task<TTransaction> BeginChangesAsync(
        System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default
    );

    Task ApplyChangesAsync(CancellationToken cancellationToken = default);

    Task DiscardChangesAsync(CancellationToken cancellationToken = default);
}
