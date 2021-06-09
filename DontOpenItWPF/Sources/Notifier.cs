using System;
using System.Drawing;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace DontOpenItWPF
{
    public static class Notifier
    {
        static Icon LoadIcon(string name)
        {
            var uri = new Uri($"pack://application:,,,/DontOpenItWPF;component/Resources/{name}", UriKind.RelativeOrAbsolute);
            var stream = Application.GetResourceStream(uri).Stream;
            return new Icon(stream);
        }

        static readonly Icon DefaultIcon = LoadIcon("Default.ico");
        static readonly Icon MuteIcon = LoadIcon("Muted.ico");

        public static void Create()
        {
            var notifyIcon = new NotifyIcon();
            notifyIcon.Icon = DefaultIcon;
            notifyIcon.Text = "Don't Open It";
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += (s, a) => MainWindow.Show();

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
            settings.Click += (s, a) => MainWindow.Show();

            var mute = new ToolStripMenuItem();
            mute.Text = Resources.Mute;
            mute.CheckOnClick = true;
            mute.CheckedChanged += (s, a) =>
            {
                App.Mute = mute.Checked;
                notifyIcon.Icon = mute.Checked ? MuteIcon : DefaultIcon;
            };

            var exit = new ToolStripMenuItem();
            exit.Text = Resources.exit;
            exit.Click += (s, a) => Application.Current.Shutdown();

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
