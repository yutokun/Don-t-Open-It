using System;
using System.Windows.Forms;

namespace DontOpenIt
{
    public partial class SettingsWindow : Form
    {
        static readonly SettingsWindow Instance = new SettingsWindow();

        SettingsWindow()
        {
            InitializeComponent();
        }

        public new static void Show() => ((Control)Instance).Show();

        void SettingsWindow_Load(object sender, EventArgs e)
        {
            beginTime.Text = Settings.Data.BeginHour.ToString();
            endTime.Text = Settings.Data.EndHour.ToString();
            removeButton.Enabled = false;
            foreach (var target in Settings.Data.Targets)
            {
                appList.Items.Add(new ListViewItem(new[] { target.Name, target.KillMethod.ToString() }));
            }
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
