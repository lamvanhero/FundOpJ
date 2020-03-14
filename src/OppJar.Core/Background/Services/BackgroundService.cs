namespace OppJar.Core.Background.Services
{
    using OppJar.Core.Background.Queues;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public class BackgroundService : BackgroundServiceBase
    {
        private readonly ILogger _logger;
        protected IBackgroundTaskQueue _taskQueue { get; }

        public BackgroundService(
            IServiceScopeFactory serviceScopeFactory,
            IBackgroundTaskQueue taskQueue,
            ILogger<BackgroundService> logger) : base(serviceScopeFactory)
        {
            _taskQueue = taskQueue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var workItem = await _taskQueue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(_serviceScopeFactory);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while handling a workItem.");
                }

                //await Task.Delay(_settings.CheckUpdateTime, stoppingToken);
            }

        }

    }
}
