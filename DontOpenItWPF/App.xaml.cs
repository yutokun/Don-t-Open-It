using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ProcessWatcher;
using MessageBox = System.Windows.MessageBox;
using MessageBoxOptions = System.Windows.MessageBoxOptions;

namespace DontOpenItWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        static readonly List<string> Confirming = new List<string>();
        public static bool Mute;

        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Startup += Start;
            app.Run();
        }

        static void Start(object sender, StartupEventArgs e)
        {
            Settings.Load();
            Notifier.Create();

            var watchdog = new Watchdog();
            watchdog.Attach(OnProcessCreated, out _);
            watchdog.Run();
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
                TimeFrame.Weekend => Messages.weekend,
                _ => throw new ArgumentOutOfRangeException(nameof(timeFrame), timeFrame, null)
            };

            var result = ShowMessage($"{Localizer.DidYouOpenApp(name)} {message}", "Don't Open It");
            if (result == MessageBoxResult.No) return false;
            result = ShowMessage(Messages.finalCheck, "Don't Open It!!!");
            if (result == MessageBoxResult.No) return false;
            return true;

            MessageBoxResult ShowMessage(string body, string caption)
            {
                return MessageBox.Show(
                    body,
                    caption,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.None,
                    MessageBoxResult.No,
                    MessageBoxOptions.DefaultDesktopOnly
                );
            }
        }
    }
}
