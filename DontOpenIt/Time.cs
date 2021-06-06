using System;

namespace DontOpenIt
{
    public enum TimeFrame
    {
        Before,
        Working,
        After,
        Holiday
    }

    public static class Time
    {
        public static TimeFrame GetTimeFrame()
        {
            var dayOfWeek = DateTime.Now.DayOfWeek;

            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                return TimeFrame.Holiday;
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
