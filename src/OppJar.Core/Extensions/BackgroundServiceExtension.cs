using Microsoft.Extensions.DependencyInjection;
using OppJar.Core.Background.Queues;
using OppJar.Core.Background.Services;

namespace OppJar.Core.Extensions
{
    public static class BackgroundServiceExtension
    {
        public static IServiceCollection AddBackgroundService(this IServiceCollection services)
        {
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

            services.AddHostedService<BackgroundService>();

            return services;
        }
    }
}
