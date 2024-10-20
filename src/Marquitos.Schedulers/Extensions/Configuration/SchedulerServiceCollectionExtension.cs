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
            services.AddSingleton(new SchedulerServiceOptions());

            return services;
        }

        /// <summary>
        /// Registers the background Scheduler service
        /// </summary>
        /// <param name="services">This Service Collection</param>
        /// <param name="options">Scheduler service configuration options</param>
        /// <returns></returns>
        public static IServiceCollection AddSchedulerService(this IServiceCollection services, SchedulerServiceOptions options)
        {
            services.AddHostedService<SchedulerService>();
            services.AddSingleton(options);

            return services;
        }

        /// <summary>
        /// Registers the background Scheduler service
        /// </summary>
        /// <param name="services">This Service Collection</param>
        /// <param name="configureOptions">Action to configure the Scheduler service configuration options</param>
        /// <returns></returns>
        public static IServiceCollection AddSchedulerService(this IServiceCollection services, Action<SchedulerServiceOptions> configureOptions)
        {
            services.AddHostedService((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<SchedulerService>>();
                var taskServices = serviceProvider.GetRequiredService<IEnumerable<IScheduledTaskService>>();
                var options = new SchedulerServiceOptions();

                configureOptions?.Invoke(options);

                var result = new SchedulerService(logger, taskServices, options);

                return result;
            });

            return services;
        }

        /// <summary>
        /// Registers the background Scheduler service
        /// </summary>
        /// <param name="services">This Service Collection</param>
        /// <param name="configureOptions">Action to configure the Scheduler service configuration options</param>
        /// <returns></returns>
        public static IServiceCollection AddSchedulerService(this IServiceCollection services, Action<IServiceProvider, SchedulerServiceOptions> configureOptions)
        {
            services.AddHostedService((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<SchedulerService>>();
                var taskServices = serviceProvider.GetRequiredService<IEnumerable<IScheduledTaskService>>();
                var options = new SchedulerServiceOptions();

                configureOptions?.Invoke(serviceProvider, options);

                var result = new SchedulerService(logger, taskServices, options);

                return result;
            });

            return services;
        }

        /// <summary>
        /// Registers the background Scheduler service
        /// </summary>
        /// <param name="services">This Service Collection</param>
        /// <param name="configureOptions">Action to configure the Scheduler service configuration options</param>
        /// <returns></returns>
        public static IServiceCollection AddSchedulerService(this IServiceCollection services, Func<IServiceProvider, SchedulerServiceOptions, Task> configureOptions)
        {
            services.AddHostedService((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<SchedulerService>>();
                var taskServices = serviceProvider.GetRequiredService<IEnumerable<IScheduledTaskService>>();
                var options = new SchedulerServiceOptions();

                configureOptions?.Invoke(serviceProvider, options).Wait();

                var result = new SchedulerService(logger, taskServices, options);

                return result;
            });

            return services;
        }

        /// <summary>
        /// Registers the background Scheduler service
        /// </summary>
        /// <param name="services">This Service Collection</param>
        /// <param name="serviceConfiguration">A configuration class that implements the ISchedulerServiceConfiguration interface.</param>
        /// <returns></returns>
        public static IServiceCollection AddSchedulerService(this IServiceCollection services, ISchedulerServiceConfiguration serviceConfiguration)
        {
            services.AddHostedService((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<SchedulerService>>();
                var taskServices = serviceProvider.GetRequiredService<IEnumerable<IScheduledTaskService>>();
                var options = new SchedulerServiceOptions();

                serviceConfiguration.ConfigureAsync(serviceProvider, options).Wait();

                var result = new SchedulerService(logger, taskServices, options);

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

        /// <summary>
        /// Register the specified ScheduledTask to run on the configured scheduled time.
        /// </summary>
        /// <param name="services">This Service Collection</param>
        /// <param name="scheduledTaskConfiguration">A class that implements the <see cref="IScheduledTaskConfiguration{T}"/> to asynchronous configure the scheduled task options.</param>
        /// <returns></returns>
        public static IServiceCollection AddScheduledTask<T>(this IServiceCollection services, IScheduledTaskConfiguration<T> scheduledTaskConfiguration) where T : class, IScheduledTask
        {
            services.AddScoped<T>();
            services.AddSingleton<IScheduledTaskService, ScheduledTaskService<T>>((serviceProvider) => {
                var logger = serviceProvider.GetRequiredService<ILogger<ScheduledTaskService<T>>>();
                var result = new ScheduledTaskService<T>(serviceProvider, logger);

                result.ConfigureOptions = async (sp, st) => {
                    if (scheduledTaskConfiguration != null)
                    {
                        await scheduledTaskConfiguration.ConfigureAsync(sp, st);
                    }
                };

                return result;
            });

            return services;
        }
    }
}
