namespace OutboxHandler.Interfaces;

internal interface IOutboxProcessor
{
    Task ProcessAsync(CancellationToken cancellationToken = default);
}
