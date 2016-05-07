using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace pctesting
{
    class ProcessControl
    {
        List<Process> processLastIteration=new List<Process>();
        List<DateTime> startTime=new List<DateTime>();
        DBService.DataServiceClient client = new DBService.DataServiceClient();

        string comp;
        string user;



        public ProcessControl(string user, string comp)
        {
            this.comp = comp;
            this.user = user;
            processLastIteration = Process.GetProcesses().ToList();
            for (int i = 0; i < processLastIteration.Count; i++)
            {
                try
                {
                    startTime.Add(processLastIteration[i].StartTime);
                }
                catch (Exception e)
                {
                    processLastIteration.Remove(processLastIteration[i]);
                    i--;
                }
            }
        }
        public void UpdateProcess()
        {
            Process[] proc = Process.GetProcesses();
            for (int i = 0; i < processLastIteration.Count;i++ )
            {
                if (!processLastIteration.Exists(lp => lp.Id == processLastIteration[i].Id))
                {
                    client.SaveProcessesToDB(processLastIteration[i].ProcessName, startTime[i], DateTime.Now, DateTime.Now - processLastIteration[i].StartTime, comp, user);
                }
            }
            startTime.Clear();
            processLastIteration = proc.ToList();
            for(int i=0;i<processLastIteration.Count;i++)
            {
                try
                {
                    startTime.Add(processLastIteration[i].StartTime);
                }
                catch(Exception e)
                {
                    processLastIteration.Remove(processLastIteration[i]);
                    i--;
                }
            }
        }

        public void SaveToDatabase()
        {
            for (int i = 0; i < processLastIteration.Count; i++)
            {
               client.SaveProcessesToDB(processLastIteration[i].ProcessName, startTime[i], DateTime.Now, DateTime.Now - startTime[i], comp, user);
            }
        }
    }
}
