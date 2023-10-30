namespace Marquitos.Schedulers.Extensions
{
    /// <summary>
    /// SchedulerService Options Extension
    /// </summary>
    public static class SchedulerServiceOptionsExtension
    {
        /// <summary>
        /// Update current options from other source options
        /// </summary>
        /// <param name="options">This options</param>
        /// <param name="sourceOptions">Source options</param>
        /// <exception cref="ArgumentNullException">Throws an ArgumentNullException if sourceOptions is null.</exception>
        public static void UpdateFrom(this SchedulerServiceOptions options, SchedulerServiceOptions sourceOptions)
        {
            if (sourceOptions == null)
            {
                throw new ArgumentNullException(nameof(sourceOptions));
            }

            options.SuppressMachineSystemEnabledSetting = sourceOptions.SuppressMachineSystemEnabledSetting;
            options.MachinesAllowedToRun = sourceOptions.MachinesAllowedToRun != null ? 
                new List<string>(sourceOptions.MachinesAllowedToRun) : new List<string>();
        }
    }
}
