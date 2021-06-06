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

            var separator = new ToolStripSeparator();

            var exit = new ToolStripMenuItem();
            exit.Text = "&Exit";
            exit.Click += (s, a) => Application.Exit();

            var menu = new ContextMenuStrip();
            menu.Items.Add(startup);
            menu.Items.Add(separator);
            menu.Items.Add(exit);
            icon.ContextMenuStrip = menu;
        }
    }
}
