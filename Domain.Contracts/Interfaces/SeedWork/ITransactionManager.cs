namespace Exemple.Identity.Domain.Contracts.Interfaces.SeedWork
{
    public interface ITransactionManager
    {
        public Task BeginChangesAsync(
            System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadUncommitted,
            CancellationToken cancellationToken = default
        );

        public Task ApplyChangesAsync(CancellationToken cancellationToken = default);

        public Task DiscardChangesAsync(CancellationToken cancellationToken = default);
    }
}
