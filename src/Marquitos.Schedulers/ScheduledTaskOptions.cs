namespace Marquitos.Schedulers
{
    /// <summary>
    /// ScheduledTask Options
    /// </summary>
    public class ScheduledTaskOptions
    {
        /// <summary>
        /// Schedule expression
        /// </summary>
        public string Schedule { get; set; } = Cron.Minutely();

        /// <summary>
        /// Base point to calculate the first occurence
        /// </summary>
        public DateTime BeginOn { get; set; } = DateTime.Now;

        /// <summary>
        /// Point in time used to calculate the last occurrence. After this point no more occurrences can run.
        /// </summary>
        public DateTime EndOn { get; set; } = DateTime.MaxValue;

        /// <summary>
        /// Indicates if the scheduled task is active and should run at given schedule.
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// If provided, the service engine will only execute the scheduled Task on those machines
        /// </summary>
        public IEnumerable<string> MachinesAllowedToRun { get; set; } = new List<string>();
    }
}
