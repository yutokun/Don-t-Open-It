using System.Drawing;
using System.Windows.Forms;

namespace DontOpenIt
{
    public static class Notifier
    {
        public static void Create()
        {
            var icon = new NotifyIcon();
            icon.Icon = new Icon("./icon.ico");
            icon.Text = "Don't Open It";
            icon.Visible = true;

            var startup = new ToolStripMenuItem();
            startup.Text = "Launch on login";
            startup.CheckOnClick = true;
            startup.Checked = Startup.HasRegistered();
            startup.CheckedChanged += (s, a) =>
            {
                if (startup.Checked)
                {
                    Startup.Register();
                }
                else
                {
                    Startup.Unregister();
                }
            };

            var mute = new ToolStripMenuItem();
            mute.Text = "ミュート";
            mute.CheckOnClick = true;
            mute.CheckedChanged += (s, a) => Program.Mute = mute.Checked;

            var exit = new ToolStripMenuItem();
            exit.Text = "&Exit";
            exit.Click += (s, a) => Application.Exit();

            var menu = new ContextMenuStrip();
            menu.Items.Add(startup);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(mute);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(exit);
            icon.ContextMenuStrip = menu;
        }
    }
}
