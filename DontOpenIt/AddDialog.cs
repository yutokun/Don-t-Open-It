using System;
using System.Windows.Forms;

namespace DontOpenIt
{
    public partial class AddDialog : Form
    {
        readonly SettingsWindow settings;

        public AddDialog(SettingsWindow parent)
        {
            settings = parent;
            InitializeComponent();
        }

        void AddDialog_Load(object sender, EventArgs e)
        {
            name.Text = Resources.appName;
            killMethod.SelectedIndex = 0;
        }

        void name_TextChanged(object sender, EventArgs e)
        {
            addButton.Enabled = !string.IsNullOrEmpty(name.Text);
        }

        void name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) addButton.PerformClick();
            if (e.KeyData == Keys.Escape) Close();
        }

        void name_KeyPress(object sender, KeyPressEventArgs e)
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
            var success = Settings.Data.AddTarget(name.Text, method);
            if (success)
            {
                settings.appList.Items.Add(new ListViewItem(new[] { name.Text, killMethod.Text }));
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
