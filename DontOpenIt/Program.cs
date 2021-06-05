using System;
using System.Diagnostics;
using System.Windows.Forms;
using ProcessWatcher;

namespace DontOpenIt
{
    internal class Program
    {
        enum TimeFrame
        {
            Before,
            Working,
            After
        }

        public static void Main(string[] args)
        {
            var watchdog = new Watchdog();
            watchdog.Attach(OnProcessCreated, out _);
            watchdog.Run();
            Console.ReadLine();
        }

        static void OnProcessCreated(Process process)
        {
            Console.WriteLine(process.ProcessName);
            if (process.ProcessName == "Slack")
            {
                var timeFrame = GetTimeFrame();
                if (timeFrame != TimeFrame.Working)
                {
                    var yes = ConfirmOpen(timeFrame, process.ProcessName);
                    if (!yes) process.Kill();
                }
            }
        }

        static TimeFrame GetTimeFrame()
        {
            var now = DateTime.Now.TimeOfDay;

            if (TimeSpan.FromHours(4) <= now && now < TimeSpan.FromHours(9))
            {
                return TimeFrame.Before;
            }

            if (TimeSpan.FromHours(4) > now || now > TimeSpan.FromHours(20))
            {
                return TimeFrame.After;
            }

            return TimeFrame.Working;
        }

        static bool ConfirmOpen(TimeFrame timeFrame, string name)
        {
            var message = "";
            switch (timeFrame)
            {
                case TimeFrame.Before:
                    message = "9時になっていませんがよろしいですか？";
                    break;

                case TimeFrame.After:
                    message = "20時を過ぎていますがよろしいですか？";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(timeFrame), timeFrame, null);
            }

            var result = MessageBox.Show($"{name} を開きましたね？ {message}", "Don't Open It", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return false;
            result = MessageBox.Show("本当に良いんですね？ 他にやることはないの？", "Don't Open It!!!", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return false;
            return true;
        }
    }
}
