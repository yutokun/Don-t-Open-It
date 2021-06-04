using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace DontOpenIt
{
    internal class Program
    {
        class Parameters
        {
            public Parameters(string path, string name, int begin, int end)
            {
                this.path = path;
                this.name = name;
                this.begin = begin;
                this.end = end;
            }

            public string path;
            public string name;
            public int begin;
            public int end;
        }

        public static void Main(string[] args)
        {
            var (success, p) = TryParseArgs(args);
            if (!success) return;

            var now = DateTime.Now.TimeOfDay;

            var timeMessage = "";
            if (now < TimeSpan.FromHours(p.begin))
            {
                timeMessage = $"{p.begin}時より早いです。";
            }
            else if (now > TimeSpan.FromHours(p.end))
            {
                timeMessage = $"{p.end}時を過ぎています。";
            }

            if (string.IsNullOrEmpty(timeMessage))
            {
                Process.Start(p.path);
            }
            else
            {
                var yes = Confirm(timeMessage, p.name);
                if (yes) Process.Start(p.path);
            }
        }

        static (bool success, Parameters parameters) TryParseArgs(string[] args)
        {
            if (args.Length != 3)
            {
                MessageBox.Show("引数の数が誤っています。\n1:起動するプログラムのパス\n2:警告の開始時間（24時間表記）\n3:警告の終了時間（24時間表記）");
                return (false, null);
            }

            var path = args[0];
            var name = "";
            if (path.EndsWith(":"))
            {
                var textInfo = CultureInfo.CurrentCulture.TextInfo;
                name = textInfo.ToTitleCase(path).Replace(":", "");
            }
            else if (!path.Contains("\\") && path.EndsWith("exe"))
            {
                var textInfo = CultureInfo.CurrentCulture.TextInfo;
                name = Path.GetFileNameWithoutExtension(textInfo.ToTitleCase(path));
            }
            else if (path.Contains("\\") && path.EndsWith("exe"))
            {
                name = Path.GetFileNameWithoutExtension(path);
            }
            else
            {
                MessageBox.Show($"指定されたパスは利用できません。\n{path}");
                return (false, null);
            }

            var beginParsed = int.TryParse(args[1], out var begin);
            var endParsed = int.TryParse(args[2], out var end);

            if (!beginParsed || !endParsed)
            {
                MessageBox.Show($"時刻指定を読み取れませんでした。\n{begin}\n{end}");
                return (false, null);
            }

            return (true, new Parameters(path, name, begin, end));
        }

        static bool Confirm(string timeMessage, string name)
        {
            var result = MessageBox.Show($"{timeMessage}本当に {name} を開いて良いですか？", "Don't Open It", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return false;
            result = MessageBox.Show("本当に良いんですね？ 他にやることはないの？", "Don't Open It!!!", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return false;
            return true;
        }
    }
}
