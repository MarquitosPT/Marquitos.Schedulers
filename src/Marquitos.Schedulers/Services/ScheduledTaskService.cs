using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCrontab;

namespace Marquitos.Schedulers.Services
{
    internal class ScheduledTaskService<T> : IScheduledTaskService where T : class, IScheduledTask
    {
        private readonly IServiceProvider _serviceProdiver;
        private readonly ILogger<ScheduledTaskService<T>> _logger;
        private readonly SemaphoreSlim _semaphore;

        private CrontabSchedule schedule;
        private ScheduledTaskOptions options;

        public ScheduledTaskService(IServiceProvider serviceProdiver, ILogger<ScheduledTaskService<T>> logger)
        {
            _serviceProdiver = serviceProdiver;
            _logger = logger;
            _semaphore = new SemaphoreSlim(1);

            schedule = CrontabSchedule.Parse(Cron.Minutely());
            options = new ScheduledTaskOptions();

            IsEnabled = false;
            IsRunning = false;
            NextRunTime = DateTime.MaxValue;
        }

        public bool IsEnabled { get; private set; }

        public bool IsRunning { get; private set; }

        public DateTime NextRunTime { get; private set; }

        public Func<IServiceProvider, ScheduledTaskOptions, Task> ConfigureOptions { get; set; } = null!;

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            if (ConfigureOptions != null)
            {
                await ConfigureOptions.Invoke(_serviceProdiver, options);
            }
            
            schedule = CrontabSchedule.Parse(options.Schedule.Trim(),
                new CrontabSchedule.ParseOptions()
                {
                    IncludingSeconds = options.Schedule.Trim().Count(e => e == ' ') == 5
                });

            
            // Prevent execution of passed ocurrence
            if (options.BeginOn < DateTime.Now)
            {
                NextRunTime = schedule.GetNextOccurrence(DateTime.Now, options.EndOn);
            }
            else
            {
                NextRunTime = schedule.GetNextOccurrence(options.BeginOn, options.EndOn);
            }

            if (options.MachinesAllowedToRun.Any())
            {
                var machineName = System.Environment.MachineName;

                IsEnabled = options.IsEnabled && options.MachinesAllowedToRun.Contains(machineName);
            }
            else
            {
                IsEnabled = options.IsEnabled;
            }

            if (NextRunTime >= options.EndOn && DateTime.Now > options.EndOn)
            {
                NextRunTime = DateTime.MaxValue;
                IsEnabled = false;
            }

            if (IsEnabled)
            {
                _logger.LogInformation("{Task} is enabled and will be triggered at {NextRunTime}.", typeof(T).Name, NextRunTime);
            }
            else
            {
                _logger.LogInformation("{Task} is not enabled.", typeof(T).Name);
            }
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            if (!IsEnabled || IsRunning)
            {
                return;
            }

            try
            {
                _semaphore.Wait(cancellationToken);
                IsRunning = true;
                var machineName = System.Environment.MachineName;

                using (var scope = _serviceProdiver.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var task = scope.ServiceProvider.GetRequiredService<T>();

                    if (NextRunTime <= DateTime.Now && NextRunTime <= options.EndOn)
                    {
                        try
                        {
                            _logger.LogInformation("Start executing {Task} on {Machine}.", task.GetType().Name, machineName);
                            try
                            {
                                await task.ExecuteAsync(cancellationToken);
                            }
                            catch (Exception e)
                            {
                                _logger.LogError(e, "{Task} running on {Machine} raised an exception: {Message}", task.GetType().Name, machineName, e.Message);
                            }
                        }
                        finally
                        {
                            NextRunTime = schedule.GetNextOccurrence(DateTime.Now, options.EndOn);

                            if (NextRunTime >= options.EndOn && DateTime.Now > options.EndOn)
                            {
                                NextRunTime = DateTime.MaxValue;
                                IsEnabled = false;
                            }

                            if (IsEnabled)
                            {
                                _logger.LogInformation("Finished executing {Task} on {Machine} and will be triggered again at {NextRunTime}.", task.GetType().Name, machineName, NextRunTime);
                            }
                            else
                            {
                                _logger.LogInformation("Finished executing {Task} on {Machine}.", task.GetType().Name, machineName);
                            }
                        }
                    }
                }

            }
            finally
            {
                IsRunning = false;
                _semaphore.Release();
            }
        }
    }
}
