using OutboxHandler.Interfaces;


namespace OutboxHandler.BackgroundServices;

internal class OutboxBackgroundService<TOutboxProcessor> : BackgroundService where TOutboxProcessor : IOutboxProcessor
{
    private readonly IServiceProvider _serviceProvider;

    public OutboxBackgroundService(
        IServiceProvider serviceProvider
    )
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateAsyncScope();

                var processor = scope.ServiceProvider.GetRequiredService<TOutboxProcessor>();

                await processor.ProcessAsync(stoppingToken);

                await Task.Delay(1000);
            }
        });

        return Task.CompletedTask;
    }
}
