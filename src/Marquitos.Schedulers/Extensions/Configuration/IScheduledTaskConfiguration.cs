namespace Marquitos.Schedulers.Extensions.Configuration
{
    /// <summary>
    /// Interface for generic task configuration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IScheduledTaskConfiguration<T> where T : class, IScheduledTask
    {
        /// <summary>
        /// Configure task options
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task ConfigureAsync(IServiceProvider serviceProvider, ScheduledTaskOptions options);
    }
}
