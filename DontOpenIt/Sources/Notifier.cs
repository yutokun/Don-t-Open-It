using System;
using System.Drawing;
using System.Windows.Forms;

namespace DontOpenIt
{
    public static class Notifier
    {
        static Icon LoadIcon(string path) => new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(path));
        static readonly Icon DefaultIcon = LoadIcon("DontOpenIt.Resources.Default.ico");
        static readonly Icon MuteIcon = LoadIcon("DontOpenIt.Resources.Muted.ico");

        public static void Create()
        {
            var notifyIcon = new NotifyIcon();
            notifyIcon.Icon = DefaultIcon;
            notifyIcon.Text = "Don't Open It";
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += (s, a) => SettingsWindow.Show();

            var startup = new ToolStripMenuItem();
            startup.Text = Resources.launchOnLogin;
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

            var settings = new ToolStripMenuItem();
            settings.Text = Resources.settings;
            settings.Click += (s, a) => SettingsWindow.Show();

            var mute = new ToolStripMenuItem();
            mute.Text = Resources.Mute;
            mute.CheckOnClick = true;
            mute.CheckedChanged += (s, a) =>
            {
                Program.Mute = mute.Checked;
                notifyIcon.Icon = mute.Checked ? MuteIcon : DefaultIcon;
            };

            var exit = new ToolStripMenuItem();
            exit.Text = Resources.exit;
            exit.Click += (s, a) => Application.Exit();

            var menu = new ContextMenuStrip();
            menu.Items.Add(startup);
            menu.Items.Add(settings);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(mute);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(exit);
            notifyIcon.ContextMenuStrip = menu;
        }
    }
}
