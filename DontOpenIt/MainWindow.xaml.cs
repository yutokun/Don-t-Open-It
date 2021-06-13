using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace DontOpenIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        const int GWL_STYLE = -16;
        const int WS_MAXIMIZEBOX = 0x10000;
        const int WS_MINIMIZEBOX = 0x20000;
        const int WM_SETICON = 0x0080;

        static MainWindow instance;

        public MainWindow()
        {
            InitializeComponent();
        }

        public new static void Show()
        {
            if (instance is { IsLoaded: true }) return;
            instance = new MainWindow();
            ((Window)instance).Show();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var handle = new WindowInteropHelper(this).Handle;
            var style = GetWindowLong(handle, GWL_STYLE);
            style &= ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX;
            SetWindowLong(handle, GWL_STYLE, style);
            SetWindowLong(handle, WM_SETICON, 0);

            BeginTime.Text = Settings.Data.BeginHour.ToString();
            EndTime.Text = Settings.Data.EndHour.ToString();
            StopWeekend.IsChecked = Settings.Data.StopWeekend;
            foreach (var target in Settings.Data.Targets)
            {
                AddToAppList(target.Name, target.KillMethod);
            }
        }

        public void AddToAppList(string name, KillMethod killMethod)
        {
            AppList.Items.Add(new { Name = name, KillMethod = killMethod });
        }

        void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveButton.IsEnabled = AppList.SelectedItem != null;
        }

        void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
        }

        void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveApp();
        }

        void MenuItemRemove_OnClick(object sender, RoutedEventArgs e)
        {
            RemoveApp();
        }

        void RemoveApp()
        {
            var index = AppList.SelectedIndex;
            AppList.Items.RemoveAt(index);
            Settings.Data.RemoveTarget(index);
        }
    }
}
