using Infrastructure.Contracts.Interfaces.Processors;


namespace Notification.Api.BackgroundServices
{
    public class OutboxBackgroundService<TOutboxProcessor> : BackgroundService where TOutboxProcessor : IOutboxProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public OutboxBackgroundService(
            IServiceProvider serviceProvider
        )
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

#pragma warning disable CS4014
            Task.Run(async () =>
#pragma warning restore CS4014
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using var scope = _serviceProvider.CreateAsyncScope();

                    var processor = scope.ServiceProvider.GetRequiredService<TOutboxProcessor>();

                    await processor.ProcessAsync(stoppingToken);

                    await Task.Delay(1000);
                }
            });
        }
    }
}
