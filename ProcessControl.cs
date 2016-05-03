using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace pctesting
{
    class ProcessControl
    {
        List<Process> processLastIteration = Process.GetProcesses().ToList();
        List<Process> ExitProcess = new List<Process>();

        string comp;
        string user;
        public ProcessControl(string user, string comp)
        {
            this.comp = comp;
            this.user = user;
        }
        public void UpdateProcess()
        {
            Process[] proc = Process.GetProcesses();
            foreach (Process pr in proc)
            {
                if (processLastIteration.Contains(pr))
                {
                    ExitProcess.Add(pr);
                }
            }
            processLastIteration = proc.ToList();
        }

        public void SaveToDatabase()
        {
            DBService.DataServiceClient client = new DBService.DataServiceClient();
            foreach (Process p in processLastIteration)
            {
                var temp=p.ExitTime-p.StartTime;
                client.SaveProcessesToDB(p.ProcessName, p.StartTime,p.ExitTime ,temp, comp, user);
            }
            foreach(Process p in ExitProcess)
            {
                var temp = p.ExitTime - p.StartTime;
                client.SaveProcessesToDB(p.ProcessName, p.StartTime, p.ExitTime, temp, comp, user);
            }
        }
    }
}
