using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DontOpenIt
{
    public partial class AddDialog : Window
    {
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        const int GWL_STYLE = -16;
        const int WS_MAXIMIZEBOX = 0x10000;
        const int WS_MINIMIZEBOX = 0x20000;
        const int WM_SETICON = 0x0080;

        public AddDialog()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var handle = new WindowInteropHelper(this).Handle;
            var style = GetWindowLong(handle, GWL_STYLE);
            style &= ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX;
            SetWindowLong(handle, GWL_STYLE, style);
            SetWindowLong(handle, WM_SETICON, 0);
        }

        void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var owner = (MainWindow)Owner;
            owner.AppList.Items.Add(new[] { ProcessName.Text, KillMethod.Text });
            Settings.Data.AddTarget(ProcessName.Text, (KillMethod)Enum.Parse(typeof(KillMethod), KillMethod.Text));
            Close();
        }
    }
}
