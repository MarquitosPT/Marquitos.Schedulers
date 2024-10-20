namespace Marquitos.Schedulers.Extensions.Configuration
{
    /// <summary>
    /// Interface for scheduler service configuration
    /// </summary>
    public interface ISchedulerServiceConfiguration
    {
        /// <summary>
        /// Configure service options
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task ConfigureAsync(IServiceProvider serviceProvider, SchedulerServiceOptions options);
    }
}
