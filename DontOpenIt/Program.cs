using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessWatcher;

namespace DontOpenIt
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static readonly List<string> Confirming = new List<string>();
        public static bool Mute;

        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
            ShowWindow(GetConsoleWindow(), 0);
            Notifier.Create();

            var watchdog = new Watchdog();
            watchdog.Attach(OnProcessCreated, out _);
            watchdog.Run();

            Application.Run();
        }

        static async void OnProcessCreated(Process process)
        {
            if (Mute) return;
            Console.WriteLine(process.ProcessName);
            if (process.ProcessName == "Slack")
            {
                if (Confirming.Contains(process.ProcessName)) return;
                Confirming.Add(process.ProcessName);

                await Task.Run(() =>
                {
                    var timeFrame = Time.GetTimeFrame();
                    if (timeFrame == TimeFrame.Working) return;

                    var yes = ConfirmOpen(timeFrame, process.ProcessName);
                    if (!yes)
                    {
                        var processes = Process.GetProcessesByName(process.ProcessName);
                        foreach (var p in processes) p.Kill();
                    }

                    Confirming.Remove(process.ProcessName);
                });
            }
        }

        static bool ConfirmOpen(TimeFrame timeFrame, string name)
        {
            var message = timeFrame switch
            {
                TimeFrame.Before => "9時になっていませんがよろしいですか？",
                TimeFrame.After => "20時を過ぎていますがよろしいですか？",
                TimeFrame.Holiday => Messages.holiday,
                _ => throw new ArgumentOutOfRangeException(nameof(timeFrame), timeFrame, null)
            };

            var result = ShowMessage($"{name} を開きましたね？ {message}", "Don't Open It");
            if (result == DialogResult.No) return false;
            result = ShowMessage(Messages.finalCheck, "Don't Open It!!!");
            if (result == DialogResult.No) return false;
            return true;

            DialogResult ShowMessage(string body, string caption)
            {
                return MessageBox.Show(
                    body,
                    caption,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.DefaultDesktopOnly
                );
            }
        }
    }
}
