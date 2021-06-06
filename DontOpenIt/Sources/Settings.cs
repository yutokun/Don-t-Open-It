using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DontOpenIt
{
    public static class Settings
    {
        public class Definition
        {
            public class Target
            {
                public string Name;
                public KillMethod KillMethod;
            }

            public int BeginHour;
            public int EndHour;
            public readonly List<Target> Targets = new List<Target>();

            public bool AddTarget(string name, KillMethod method)
            {
                if (TargetApps.Contains(name)) return false;

                Targets.Add(new Target { Name = name, KillMethod = method });
                Save();
                return true;
            }

            public void RemoveTarget(string name)
            {
                var target = Targets.FirstOrDefault(t => t.Name == name);
                if (target != null)
                {
                    Targets.Remove(target);
                    Save();
                }
            }
        }

        public static Definition Data { get; private set; }
        public static IEnumerable<string> TargetApps => Data.Targets.Select(t => t.Name);
        public static Definition.Target GetTarget(string name) => Data.Targets.First(t => t.Name == name);
        static string FilePath => Path.Combine(Directory.GetParent(Path.GetTempFileName()).FullName, "DontOpenIt", "Settings.xml");

        public static void Load()
        {
            if (File.Exists(FilePath))
            {
                var serializer = new XmlSerializer(typeof(Definition));
                using var sr = new StreamReader(FilePath, Encoding.UTF8);
                Data = serializer.Deserialize(sr) as Definition;
            }
            else
            {
                Create();
            }
        }

        static void Create()
        {
            Data = new Definition { BeginHour = 9, EndHour = 20 };
            Data.AddTarget("Slack", KillMethod.Kill);
            Data.AddTarget("Unity Hub", KillMethod.CloseMainWindow);
            Save();
        }

        public static void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            var serializer = new XmlSerializer(typeof(Definition));
            using var sw = new StreamWriter(FilePath, false, Encoding.UTF8);
            serializer.Serialize(sw, Data);
        }
    }
}
