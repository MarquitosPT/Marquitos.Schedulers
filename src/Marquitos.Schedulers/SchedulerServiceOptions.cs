namespace Marquitos.Schedulers
{
    /// <summary>
    /// SchedulerService Options
    /// </summary>
    public class SchedulerServiceOptions
    {
        /// <summary>
        /// Suppress machine system enabled setting (SCHEDULER_SERVICE_ENABLED)
        /// </summary>
        public bool SuppressMachineSystemEnabledSetting { get; set; } = false;

        /// <summary>
        /// If provided, the service engine will only execute the scheduled Task on those machines
        /// </summary>
        public IEnumerable<string> MachinesAllowedToRun { get; set; } = new List<string>();
    }
}
