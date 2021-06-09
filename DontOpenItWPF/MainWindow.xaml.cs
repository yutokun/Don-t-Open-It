using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DontOpenItWPF
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
        }
    }
}
