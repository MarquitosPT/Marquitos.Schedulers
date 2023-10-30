using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Marquitos.Schedulers.Services
{
    internal class SchedulerService : BackgroundService
    {
        private readonly ILogger<SchedulerService> _logger;
        private readonly IEnumerable<IScheduledTaskService> _services;
        private readonly SchedulerServiceOptions _options;

        public SchedulerService(ILogger<SchedulerService> logger, IEnumerable<IScheduledTaskService> services, SchedulerServiceOptions options)
        {
            _logger = logger;
            _services = services;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var machineName = System.Environment.MachineName;

            if (!IsEnabled())
            {
                _logger.LogInformation("{SchedulerService} is disabled on {Machine}.", nameof(SchedulerService), machineName);

                return;
            }

            // wait 15 seconds before start
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);

            _logger.LogInformation("{SchedulerService} is starting on {Machine}.", nameof(SchedulerService), machineName);

            stoppingToken.Register(() =>
                _logger.LogInformation("{SchedulerService} is stopping on {Machine}.", nameof(SchedulerService), machineName));

            // Initialize Schedulers
            foreach (var item in _services)
            {
                await item.InitializeAsync(stoppingToken);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var taskFactory = new TaskFactory(TaskScheduler.Current);
                    var referenceTime = DateTime.Now;

                    foreach (var item in _services.Where(e => e.IsEnabled && e.NextRunTime <= referenceTime && !e.IsRunning))
                    {
                        await taskFactory.StartNew(
                            async () =>
                            {
                                await item.ExecuteAsync(stoppingToken);
                            },
                            stoppingToken).ConfigureAwait(false);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "{SchedulerService} running on {Machine} raised an exception: {Message}", nameof(SchedulerService), machineName, e.Message);
                }

                // wait 1 second
                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogInformation("{SchedulerService} is stopping on {Machine}.", nameof(SchedulerService), machineName);
        }

        private bool IsEnabled()
        {    
            if (_options.SuppressMachineSystemEnabledSetting)
            {
                if (_options.MachinesAllowedToRun.Any())
                {
                    return _options.MachinesAllowedToRun.Contains(System.Environment.MachineName);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                var envSetting = System.Environment.GetEnvironmentVariable("SCHEDULER_SERVICE_ENABLED");

                if (envSetting != null && bool.Parse(envSetting) == false)
                {
                    return false;
                }
                else
                {
                    if (_options.MachinesAllowedToRun.Any())
                    {
                        return _options.MachinesAllowedToRun.Contains(System.Environment.MachineName);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}
