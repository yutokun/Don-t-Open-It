using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
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

        static readonly string[] ExcludedProcesses = { "svchost", "ApplicationFrameworkHost", "audiodg", "BtwRSupportService", "conhost", "csrss", "dllhost", "fontdrvhost", "MSBuild", "MsMpEng", "secd", "sihost", "System", "SystemSettings", "wininit", "winlogon", "WUDFHost" };

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

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            var currentProcesses = Process.GetProcesses()
                                          .Select(p => p.ProcessName)
                                          .Distinct()
                                          .Except(ExcludedProcesses) 
                                          .Except(Settings.TargetApps)
                                          .OrderBy(p => p)
                                          .ToArray();
            foreach (var process in currentProcesses)
            {
                ProcessName.Items.Add(process);
            }

            if (string.IsNullOrEmpty(ProcessName.Text)) ProcessName.SelectedIndex = 0;
            ProcessName.Focus();
        }

        void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var killMethod = (KillMethod)Enum.Parse(typeof(KillMethod), KillMethod.Text);
            var success = Settings.Data.AddTarget(ProcessName.Text, killMethod);
            if (success)
            {
                ((MainWindow)Owner).AddToAppList(ProcessName.Text, killMethod);
                Close();
            }
            else
            {
                MessageBox.Show(Properties.Resources.appAlreadyAdded);
            }
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) AddButton_Click(sender, e);
            if (e.Key == Key.Escape) Close();
        }
    }
}
