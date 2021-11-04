using System;

namespace DontOpenIt
{
    public enum TimeFrame
    {
        Before,
        Working,
        After,
        Weekend
    }

    public static class Time
    {
        public static TimeFrame GetTimeFrame()
        {
            var dayOfWeek = DateTime.Now.DayOfWeek;

            if (Settings.Data.StopWeekend && dayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                return TimeFrame.Weekend;
            }

            var now = DateTime.Now.TimeOfDay;

            if (TimeSpan.FromHours(4) <= now && now < TimeSpan.FromHours(Settings.Data.BeginHour))
            {
                return TimeFrame.Before;
            }

            if (TimeSpan.FromHours(4) > now || now > TimeSpan.FromHours(Settings.Data.EndHour))
            {
                return TimeFrame.After;
            }

            return TimeFrame.Working;
        }
    }
}
