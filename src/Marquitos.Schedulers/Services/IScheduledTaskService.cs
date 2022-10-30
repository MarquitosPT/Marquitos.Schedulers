namespace Marquitos.Schedulers.Services
{
    internal interface IScheduledTaskService
    {
        DateTime NextRunTime { get; }

        bool IsEnabled { get; }

        bool IsRunning { get; }

        Task InitializeAsync(CancellationToken cancellationToken = default);

        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
