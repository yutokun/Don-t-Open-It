using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MessageBox = System.Windows.MessageBox;
using MessageBoxOptions = System.Windows.MessageBoxOptions;

namespace DontOpenIt
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

        static async void Start(object sender, StartupEventArgs e)
        {
            Settings.Load();
            Notifier.Create();

            var watcher = new ProcessWatcher();
            watcher.OnNewProcessLaunch += OnProcessCreated;
            await watcher.Run();
        }

        static async void OnProcessCreated(IEnumerable<string> processes)
        {
            if (Mute) return;

            foreach (var process in processes)
            {
                Debug.WriteLine(process);
                if (Settings.TargetApps.Contains(process) && !Confirming.Contains(process))
                {
                    Confirming.Add(process);

                    await Task.Run(() =>
                    {
                        var timeFrame = Time.GetTimeFrame();
                        if (timeFrame == TimeFrame.Working) return;

                        var yes = ConfirmOpen(timeFrame, process);
                        if (!yes)
                        {
                            var processes = Process.GetProcessesByName(process);
                            foreach (var p in processes)
                            {
                                switch (Settings.GetTarget(process).KillMethod)
                                {
                                    case KillMethod.CloseMainWindow:
                                        p.CloseMainWindow();
                                        break;

                                    case KillMethod.Kill:
                                        p.Kill();
                                        break;

                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }
                            }
                        }

                        Confirming.Remove(process);
                    });
                }
            }
        }

        static bool ConfirmOpen(TimeFrame timeFrame, string name)
        {
            var message = timeFrame switch
            {
                TimeFrame.Before => Localizer.BeforeWorkingTime(Settings.Data.BeginHour),
                TimeFrame.After => Localizer.AfterWorkingTime(Settings.Data.EndHour),
                TimeFrame.Weekend => DontOpenIt.Properties.Resources.weekend,
                _ => throw new ArgumentOutOfRangeException(nameof(timeFrame), timeFrame, null)
            };

            var result = ShowMessage($"{Localizer.DidYouOpenApp(name)} {message}", "Don't Open It");
            if (result == MessageBoxResult.No) return false;
            result = ShowMessage(DontOpenIt.Properties.Resources.finalCheck, "Don't Open It!!!");
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
