using System.Windows.Forms;
using Microsoft.Win32;

namespace DontOpenIt
{
    public static class Startup
    {
        static RegistryKey Registry => Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

        public static void Register()
        {
            using var r = Registry;
            r.SetValue(Application.ProductName, Application.ExecutablePath);
        }

        public static void Unregister()
        {
            using var r = Registry;
            r.DeleteValue(Application.ProductName);
        }

        public static bool HasRegistered()
        {
            using var r = Registry;
            return r.GetValue(Application.ProductName) != null;
        }
    }
}
