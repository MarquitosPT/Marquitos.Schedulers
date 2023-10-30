namespace Marquitos.Schedulers.Extensions
{
    /// <summary>
    /// ScheduledTask Options Extension
    /// </summary>
    public static class ScheduledTaskOptionsExtension
    {
        /// <summary>
        /// Update current options from other source options
        /// </summary>
        /// <param name="options">This options</param>
        /// <param name="sourceOptions">Source options</param>
        /// <exception cref="ArgumentNullException">Throws an ArgumentNullException if sourceOptions is null.</exception>
        public static void UpdateFrom(this ScheduledTaskOptions options, ScheduledTaskOptions sourceOptions)
        {
            if (sourceOptions == null)
            {
                throw new ArgumentNullException(nameof(sourceOptions));
            }

            options.BeginOn = sourceOptions.BeginOn;
            options.EndOn = sourceOptions.EndOn;
            options.Schedule = sourceOptions.Schedule;
            options.IsEnabled = sourceOptions.IsEnabled;
            options.MachinesAllowedToRun = sourceOptions.MachinesAllowedToRun != null ? 
                new List<string>(sourceOptions.MachinesAllowedToRun) : new List<string>();
        }
    }
}
