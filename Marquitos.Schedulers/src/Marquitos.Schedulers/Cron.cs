﻿using System;

namespace Marquitos.Schedulers
{
    /// <summary>
    /// Helper class that provides common values for the cron expressions.
    /// </summary>
    public static class Cron
    {
        /// <summary>
        /// Returns cron expression that fires every minute.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Minutely()
        {
            return "* * * * *";
        }

        /// <summary>
        /// Returns cron expression that fires every hour at the first minute.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Hourly()
        {
            return Hourly(minute: 0);
        }

        /// <summary>
        /// Returns cron expression that fires every hour at the specified minute.
        /// </summary>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        /// <returns>System.String.</returns>
        public static string Hourly(int minute)
        {
            return $"{minute} * * * *";
        }

        /// <summary>
        /// Returns cron expression that fires every day at 00:00 UTC.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Daily()
        {
            return Daily(hour: 0);
        }

        /// <summary>
        /// Returns cron expression that fires every day at the first minute of
        /// the specified hour in UTC.
        /// </summary>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <returns>System.String.</returns>
        public static string Daily(int hour)
        {
            return Daily(hour, minute: 0);
        }

        /// <summary>
        /// Returns cron expression that fires every day at the specified hour and minute
        /// in UTC.
        /// </summary>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        /// <returns>System.String.</returns>
        public static string Daily(int hour, int minute)
        {
            return $"{minute} {hour} * * *";
        }

        /// <summary>
        /// Returns cron expression that fires every week at Monday, 00:00 UTC.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Weekly()
        {
            return Weekly(DayOfWeek.Monday);
        }

        /// <summary>
        /// Returns cron expression that fires every week at 00:00 UTC of the specified
        /// day of the week.
        /// </summary>
        /// <param name="dayOfWeek">The day of week in which the schedule will be activated.</param>
        /// <returns>System.String.</returns>
        public static string Weekly(DayOfWeek dayOfWeek)
        {
            return Weekly(dayOfWeek, hour: 0);
        }

        /// <summary>
        /// Returns cron expression that fires every week at the first minute
        /// of the specified day of week and hour in UTC.
        /// </summary>
        /// <param name="dayOfWeek">The day of week in which the schedule will be activated.</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <returns>System.String.</returns>
        public static string Weekly(DayOfWeek dayOfWeek, int hour)
        {
            return Weekly(dayOfWeek, hour, minute: 0);
        }

        /// <summary>
        /// Returns cron expression that fires every week at the specified day
        /// of week, hour and minute in UTC.
        /// </summary>
        /// <param name="dayOfWeek">The day of week in which the schedule will be activated.</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        /// <returns>System.String.</returns>
        public static string Weekly(DayOfWeek dayOfWeek, int hour, int minute)
        {
            return $"{minute} {hour} * * {(int)dayOfWeek}";
        }

        /// <summary>
        /// Returns cron expression that fires every month at 00:00 UTC of the first
        /// day of month.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Monthly()
        {
            return Monthly(day: 1);
        }

        /// <summary>
        /// Returns cron expression that fires every month at 00:00 UTC of the specified
        /// day of month.
        /// </summary>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        /// <returns>System.String.</returns>
        public static string Monthly(int day)
        {
            return Monthly(day, hour: 0);
        }

        /// <summary>
        /// Returns cron expression that fires every month at the first minute of the
        /// specified day of month and hour in UTC.
        /// </summary>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <returns>System.String.</returns>
        public static string Monthly(int day, int hour)
        {
            return Monthly(day, hour, minute: 0);
        }

        /// <summary>
        /// Returns cron expression that fires every month at the specified day of month,
        /// hour and minute in UTC.
        /// </summary>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        /// <returns>System.String.</returns>
        public static string Monthly(int day, int hour, int minute)
        {
            return $"{minute} {hour} {day} * *";
        }

        /// <summary>
        /// Returns cron expression that fires every year on Jan, 1st at 00:00 UTC.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Yearly()
        {
            return Yearly(month: 1);
        }

        /// <summary>
        /// Returns cron expression that fires every year in the first day at 00:00 UTC
        /// of the specified month.
        /// </summary>
        /// <param name="month">The month in which the schedule will be activated (1-12).</param>
        /// <returns>System.String.</returns>
        public static string Yearly(int month)
        {
            return Yearly(month, day: 1);
        }

        /// <summary>
        /// Returns cron expression that fires every year at 00:00 UTC of the specified
        /// month and day of month.
        /// </summary>
        /// <param name="month">The month in which the schedule will be activated (1-12).</param>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        /// <returns>System.String.</returns>
        public static string Yearly(int month, int day)
        {
            return Yearly(month, day, hour: 0);
        }

        /// <summary>
        /// Returns cron expression that fires every year at the first minute of the
        /// specified month, day and hour in UTC.
        /// </summary>
        /// <param name="month">The month in which the schedule will be activated (1-12).</param>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <returns>System.String.</returns>
        public static string Yearly(int month, int day, int hour)
        {
            return Yearly(month, day, hour, minute: 0);
        }

        /// <summary>
        /// Returns cron expression that fires every year at the specified month, day,
        /// hour and minute in UTC.
        /// </summary>
        /// <param name="month">The month in which the schedule will be activated (1-12).</param>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        /// <returns>System.String.</returns>
        public static string Yearly(int month, int day, int hour, int minute)
        {
            return $"{minute} {hour} {day} {month} *";
        }

        /// <summary>
        /// Returns cron expression that never fires. Specifically 31st of February
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Never()
        {
            return Yearly(2, 31);
        }
    }
}
