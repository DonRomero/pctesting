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

            public void UpdateProcess()
            {
                Process[] proc = Process.GetProcesses();
                foreach (Process pr in proc)
                {
                    if(processLastIteration.Contains(pr))
                    {
                        ExitProcess.Add(pr);
                    }
                }
                processLastIteration = proc.ToList();
            }
        }

}
