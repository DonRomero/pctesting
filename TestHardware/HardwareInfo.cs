using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;

namespace pctesting.TestHardware
{
    class HardwareInfo
    {
        public IEnumerable<string> Info
        {
            get
            {
                return hardwareInfo;
            }
            protected set
            {
                hardwareInfo = (IList<string>)value;
            }
        }
        private static string QueryWin32OperatingSystem
        {
            get
            {
                return "SELECT * FROM Win32_OperatingSystem";
            }
        }
        private static string QueryWin32Processor
        {
            get
            {
                return "SELECT * FROM Win32_Processor";
            }
        }
        private static string QueryWin32VideoController
        {
            get
            {
                return "SELECT * FROM Win32_VideoController";
            }
        }
        private IList<string> hardwareInfo = new List<string>();
        private ManagementObjectSearcher find;
        //public HardwareInfo()
        //{
        //    GetMemoryInformation();
        //    GetProcessorInformation();
        //}
        public string GetMemoryInformation()
        {
            find = new ManagementObjectSearcher(QueryWin32OperatingSystem);
            ManagementObjectCollection.ManagementObjectEnumerator it = find.Get().GetEnumerator();
            while (it.MoveNext())
            {
            }
            return Convert.ToString(it.Current["TotalVisibleMemorySize"]);
        }
        public string GetFreeMemoryInformation()
        {
            find = new ManagementObjectSearcher(QueryWin32OperatingSystem);
            ManagementObjectCollection.ManagementObjectEnumerator it = find.Get().GetEnumerator();
            while (it.MoveNext())
            {
            }
            return Convert.ToString(it.Current["FreePhysicalMemory"]);
        }
        public int GetProcessorInformation()
        {
            find = new ManagementObjectSearcher(QueryWin32Processor);
            ManagementObjectCollection.ManagementObjectEnumerator it = find.Get().GetEnumerator();
            while (it.MoveNext())
            {
                //hardwareInfo.Add(String.Format("Загрузка процессора: {0}%", it.Current["LoadPercentage"]));
            }
            return Convert.ToInt32(it.Current["LoadPercentage"]);
        }
        public string GetVideoRam()
        {
            find = new ManagementObjectSearcher(QueryWin32VideoController);
            ManagementObjectCollection.ManagementObjectEnumerator it = find.Get().GetEnumerator();
            while (it.MoveNext())
            {

            }
            return Convert.ToString(it.Current["AdapterRAM"]);
        }
    }
}
