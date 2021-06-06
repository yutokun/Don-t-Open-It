using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            Settings.Load();
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
            if (Settings.TargetApps.Contains(process.ProcessName) && !Confirming.Contains(process.ProcessName))
            {
                Confirming.Add(process.ProcessName);

                await Task.Run(() =>
                {
                    var timeFrame = Time.GetTimeFrame();
                    if (timeFrame == TimeFrame.Working) return;

                    var yes = ConfirmOpen(timeFrame, process.ProcessName);
                    if (!yes)
                    {
                        var processes = Process.GetProcessesByName(process.ProcessName);
                        foreach (var p in processes)
                        {
                            switch (Settings.GetTarget(process.ProcessName).KillMethod)
                            {
                                case KillMethod.CloseMainWindow:
                                    p.CloseMainWindow();
                                    break;

                                case KillMethod.Close:
                                    p.Close();
                                    break;

                                case KillMethod.Kill:
                                    p.Kill();
                                    break;

                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }

                    Confirming.Remove(process.ProcessName);
                });
            }
        }

        static bool ConfirmOpen(TimeFrame timeFrame, string name)
        {
            var message = timeFrame switch
            {
                TimeFrame.Before => Localizer.BeforeWorkingTime(Settings.Data.BeginHour),
                TimeFrame.After => Localizer.AfterWorkingTime(Settings.Data.EndHour),
                TimeFrame.Holiday => Messages.holiday,
                _ => throw new ArgumentOutOfRangeException(nameof(timeFrame), timeFrame, null)
            };

            var result = ShowMessage($"{Localizer.DidYouOpenApp(name)} {message}", "Don't Open It");
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
