using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace DontOpenIt
{
    public partial class AddDialog : Form
    {
        readonly SettingsWindow settings;

        static readonly string[] ExcludedProcesses = { "svchost", "ApplicationFrameworkHost", "audiodg", "BtwRSupportService", "conhost", "csrss", "dllhost", "fontdrvhost", "MSBuild", "MsMpEng", "secd", "sihost", "System", "SystemSettings", "wininit", "winlogon", "WUDFHost" };

        public AddDialog(SettingsWindow parent)
        {
            settings = parent;
            InitializeComponent();
        }

        void AddDialog_Load(object sender, EventArgs e)
        {
            var currentProcesses = Process.GetProcesses()
                                          .Select(p => p.ProcessName)
                                          .Distinct()
                                          .Except(ExcludedProcesses)
                                          .Except(Settings.TargetApps)
                                          .ToArray();
            processes.Items.Clear();
            processes.Items.AddRange(currentProcesses);
            processes.SelectedIndex = 0;
            killMethod.SelectedIndex = 0;
        }

        void processes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) addButton.PerformClick();
            if (e.KeyData == Keys.Escape) Close();
        }

        void processes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Escape) e.Handled = true;
        }

        void killMethod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) addButton.PerformClick();
            if (e.KeyData == Keys.Escape) Close();
        }

        void addButton_Click(object sender, EventArgs e)
        {
            var method = (KillMethod)Enum.Parse(typeof(KillMethod), killMethod.Text);
            var success = Settings.Data.AddTarget(processes.Text, method);
            if (success)
            {
                settings.appList.Items.Add(new ListViewItem(new[] { processes.Text, killMethod.Text }));
                Close();
            }
            else
            {
                MessageBox.Show(Messages.appAlreadyAdded);
            }
        }

        void addButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape) Close();
        }
    }
}
