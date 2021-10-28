using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DontOpenIt
{
    public class ProcessWatcher
    {
        HashSet<string> processes = new HashSet<string>();
        static HashSet<string> Processes => Process.GetProcesses().Select(p => p.ProcessName).ToHashSet();
        public event Action<IEnumerable<string>> OnNewProcessLaunch;

        public async Task Run(int intervalMs = 2000)
        {
            processes = Processes;
            while (true)
            {
                await Task.Delay(intervalMs);
                var added = Processes.Except(processes).ToArray();
                if (added.Any()) OnNewProcessLaunch?.Invoke(added);
                processes = Processes;
            }
        }
    }
}
