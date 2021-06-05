using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessWatcher;

namespace DontOpenIt
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        enum TimeFrame
        {
            Before,
            Working,
            After
        }

        static readonly List<string> Confirming = new List<string>();

        public static void Main(string[] args)
        {
            ShowWindow(GetConsoleWindow(), 0);
            CreateNotifyIcon();

            var watchdog = new Watchdog();
            watchdog.Attach(OnProcessCreated, out _);
            watchdog.Run();
            Application.Run();
        }

        static void CreateNotifyIcon()
        {
            var icon = new NotifyIcon();
            icon.Icon = Icon.FromHandle(GetConsoleWindow());
            icon.Text = "Don't Open It";
            icon.Visible = true;

            var menu = new ContextMenuStrip();

            var exit = new ToolStripMenuItem();
            exit.Text = "&Exit";
            exit.Click += (s, a) => Application.Exit();

            menu.Items.Add(exit);
            icon.ContextMenuStrip = menu;
        }

        static async void OnProcessCreated(Process process)
        {
            Console.WriteLine(process.ProcessName);
            if (process.ProcessName == "Slack")
            {
                if (Confirming.Contains(process.ProcessName)) return;
                Confirming.Add(process.ProcessName);

                await Task.Run(() =>
                {
                    var timeFrame = GetTimeFrame();
                    if (timeFrame != TimeFrame.Working)
                    {
                        var yes = ConfirmOpen(timeFrame, process.ProcessName);
                        if (!yes)
                        {
                            var processes = Process.GetProcessesByName(process.ProcessName);
                            foreach (var p in processes) p.Kill();
                        }

                        Confirming.Remove(process.ProcessName);
                    }
                });
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
