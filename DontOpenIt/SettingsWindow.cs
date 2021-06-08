using System;
using System.Windows.Forms;

namespace DontOpenIt
{
    public partial class SettingsWindow : Form
    {
        static readonly SettingsWindow Instance = new SettingsWindow();
        ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

        SettingsWindow()
        {
            InitializeComponent();
        }

        public new static void Show() => ((Control)Instance).Show();

        void SettingsWindow_Load(object sender, EventArgs e)
        {
            beginTime.Text = Settings.Data.BeginHour.ToString();
            endTime.Text = Settings.Data.EndHour.ToString();
            weekend.Checked = Settings.Data.StopWeekend;
            removeButton.Enabled = false;
            foreach (var target in Settings.Data.Targets)
            {
                appList.Items.Add(new ListViewItem(new[] { target.Name, target.KillMethod.ToString() }));
            }

            var delete = new ToolStripMenuItem();
            delete.Text = Resources.remove;
            delete.Click += removeButton_Click;
            contextMenuStrip.Items.Add(delete);
        }

        void beginTime_TextChanged(object sender, EventArgs e)
        {
            var parsed = int.TryParse(beginTime.Text, out var time);
            if (parsed)
            {
                Settings.Data.BeginHour = time;
                Settings.Save();
            }
        }

        void endTime_TextChanged(object sender, EventArgs e)
        {
            var parsed = int.TryParse(endTime.Text, out var time);
            if (parsed)
            {
                Settings.Data.EndHour = time;
                Settings.Save();
            }
        }

        void addButton_Click(object sender, EventArgs e)
        {
            var dialog = new AddDialog(this);
            dialog.ShowDialog(this);
        }

        void removeButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in appList.SelectedItems)
            {
                Settings.Data.RemoveTarget(item.Text);
                appList.Items.Remove(item);
            }
        }

        void appList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            removeButton.Enabled = e.IsSelected;
        }

        void appList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip.Show(Cursor.Position);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason == CloseReason.WindowsShutDown) return;

            e.Cancel = true;
            Hide();
        }

        void weekend_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Data.StopWeekend = weekend.Checked;
            Settings.Save();
        }
    }
}
