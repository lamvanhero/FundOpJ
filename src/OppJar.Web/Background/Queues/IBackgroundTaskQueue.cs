namespace OppJar.Web.Background.Queues
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<IServiceScopeFactory, Task> workItem);

        Task<Func<IServiceScopeFactory, Task>> DequeueAsync(
            CancellationToken cancellationToken);
    }
}
