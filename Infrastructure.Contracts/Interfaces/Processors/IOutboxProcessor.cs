namespace Infrastructure.Contracts.Interfaces.Processors;

public interface IOutboxProcessor
{
    Task ProcessAsync(CancellationToken cancellationToken = default);
}
