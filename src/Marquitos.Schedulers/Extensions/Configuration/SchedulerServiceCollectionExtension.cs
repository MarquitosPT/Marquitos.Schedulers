using Marquitos.Schedulers.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Marquitos.Schedulers.Extensions.Configuration
{
    /// <summary>
    /// Scheduler ServiceCollection Extension
    /// </summary>
    public static class SchedulerServiceCollectionExtension
    {
        /// <summary>
        /// Registers the background Scheduler service
        /// </summary>
        /// <param name="services">This Service Collection</param>
        /// <returns></returns>
        public static IServiceCollection AddSchedulerService(this IServiceCollection services)
        {
            services.AddHostedService<SchedulerService>();

            return services;
        }

        /// <summary>
        /// Register the specified ScheduledTask to run on the configured scheduled time.
        /// </summary>
        /// <param name="services">This Service Collection</param>
        /// <param name="configureOptions">Action to configure the scheduled task options.</param>
        /// <returns></returns>
        public static IServiceCollection AddScheduledTask<T>(this IServiceCollection services, Action<ScheduledTaskOptions> configureOptions) where T : class, IScheduledTask
        {
            services.AddScoped<T>();
            services.AddSingleton<IScheduledTaskService, ScheduledTaskService<T>>((serviceProvider) => {
                var logger = serviceProvider.GetRequiredService<ILogger<ScheduledTaskService<T>>>();
                var result = new ScheduledTaskService<T>(serviceProvider, logger);

                result.ConfigureOptions = async (sp, st) => {

                    configureOptions?.Invoke(st);

                    await Task.CompletedTask;
                };

                return result;
            });

            return services;
        }

        /// <summary>
        /// Register the specified ScheduledTask to run on the configured scheduled time.
        /// </summary>
        /// <param name="services">This Service Collection</param>
        /// <param name="configureOptions">Action to configure the scheduled task options.</param>
        /// <returns></returns>
        public static IServiceCollection AddScheduledTask<T>(this IServiceCollection services, Action<IServiceProvider, ScheduledTaskOptions> configureOptions) where T : class, IScheduledTask
        {
            services.AddScoped<T>();
            services.AddSingleton<IScheduledTaskService, ScheduledTaskService<T>>((serviceProvider) => {
                var logger = serviceProvider.GetRequiredService<ILogger<ScheduledTaskService<T>>>();
                var result = new ScheduledTaskService<T>(serviceProvider, logger);

                result.ConfigureOptions = async (sp, st) => {

                    configureOptions?.Invoke(sp, st);

                    await Task.CompletedTask;
                };

                return result;
            });

            return services;
        }

        /// <summary>
        /// Register the specified ScheduledTask to run on the configured scheduled time.
        /// </summary>
        /// <param name="services">This Service Collection</param>
        /// <param name="configureOptions">Function to asynchronous configure the scheduled task options.</param>
        /// <returns></returns>
        public static IServiceCollection AddScheduledTask<T>(this IServiceCollection services, Func<IServiceProvider, ScheduledTaskOptions, Task> configureOptions) where T : class, IScheduledTask
        {
            services.AddScoped<T>();
            services.AddSingleton<IScheduledTaskService, ScheduledTaskService<T>>((serviceProvider) => {
                var logger = serviceProvider.GetRequiredService<ILogger<ScheduledTaskService<T>>>();
                var result = new ScheduledTaskService<T>(serviceProvider, logger);

                result.ConfigureOptions = async (sp, st) => {
                    if (configureOptions != null)
                    {
                        await configureOptions.Invoke(sp, st);
                    }
                };

                return result;
            });

            return services;
        }
    }
}
