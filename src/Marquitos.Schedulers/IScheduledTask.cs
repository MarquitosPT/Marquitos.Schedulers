namespace Marquitos.Schedulers
{
    /// <summary>
    /// Interface for scheduled tasks
    /// </summary>
    public interface IScheduledTask
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        /// <remarks>This method is internaly called. It should dot be called directly.</remarks>
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
