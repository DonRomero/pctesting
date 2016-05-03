using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace pctesting
{
    class ProcessControl
    {
        List<Process> processLastIteration = Process.GetProcesses().ToList();
        List<Process> ExitProcess = new List<Process>();

        string comp;
        string user;

        [DllImport("kernel32.dll")]
        static extern bool GetProcessTimes(IntPtr handle,
            out System.Runtime.InteropServices.FILETIME lpCreationTime, 
            out System.Runtime.InteropServices.FILETIME lpExitTime, 
            out System.Runtime.InteropServices.FILETIME lpKernelTime,
            out System.Runtime.InteropServices.FILETIME lpUserTime);

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
                //FILETIME ftCreation, ftExit, ftKernel, ftUser;
                //try
                //{                   
                //    GetProcessTimes(pr.Handle, out ftCreation, out ftExit, out ftKernel, out ftUser);
                //}
                //catch(Exception e)
                //{

                //}
                if (!processLastIteration.Exists(lp=>lp.Id==pr.Id))
                {
                    ExitProcess.Add(pr);
                }
            }
            processLastIteration = proc.ToList();
        }

        public void SaveToDatabase()
        {
            DBService.DataServiceClient client = new DBService.DataServiceClient();
            //foreach (Process p in processLastIteration)
            //{
            //    var temp=p.ExitTime-p.StartTime;
            //    client.SaveProcessesToDB(p.ProcessName, p.StartTime,p.ExitTime ,temp, comp, user);
            //}
            foreach(Process p in ExitProcess)
            {
                var temp = p.ExitTime - p.StartTime;
                client.SaveProcessesToDB(p.ProcessName, p.StartTime, p.ExitTime, temp, comp, user);
            }
        }
    }
}
