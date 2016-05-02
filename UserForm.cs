using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pctesting.TestHardware;

namespace pctesting
{
    public partial class UserForm : Form
    {
        FileManager fileWatcher;
        TrafficManager trafficWatcher;
        ProcessControl process = new ProcessControl();
        KeyHook kh = new KeyHook();
        public UserForm(string userName)
        {
            InitializeComponent();
            fileWatcher = new FileManager(Environment.MachineName, userName);
            trafficWatcher = new TrafficManager(Environment.MachineName, userName);
            fileWatcher.watch();
            trafficWatcher.Start();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void UserForm_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckEnable enable = new CheckEnable();
            HardwareInfo hardware = new HardwareInfo();
            List<myThread> threads = new List<myThread>();
            int i = 0;
            int width = 1360;
            int height = 800;
            int width1 = 1360;
            int height1 = 800;
            int ColichChain = 0;
            while (hardware.GetProcessorInformation() < 100)
            {
                if (i % 2 == 1)
                {
                    height1 = height1 / 2;
                    int h = 0;
                    while (h < height)
                    {
                        int w = 0;
                        while (w < width)
                        {
                            if (enable.Active)
                            {
                                threads.Add(new myThread(w, h, width1, height1, enable));
                                ColichChain++;
                                if (hardware.GetProcessorInformation() == 100)
                                {
                                    string CPU_load = Convert.ToString(hardware.GetProcessorInformation());
                                    string Ram = hardware.GetMemoryInformation();
                                    string FreeRam = hardware.GetFreeMemoryInformation();
                                    string VideoRam = hardware.GetVideoRam();
                                    foreach (myThread k in threads)
                                    {
                                        k.Stop();
                                    }
                                    Report ot = new Report(ColichChain, CPU_load, Ram, FreeRam, VideoRam);
                                    ot.ShowDialog();
                                    return;
                                }
                            }
                            else
                            {
                                string CPU_load = Convert.ToString(hardware.GetProcessorInformation());
                                string Ram = hardware.GetMemoryInformation();
                                string FreeRam = hardware.GetFreeMemoryInformation();
                                string VideoRam = hardware.GetVideoRam();
                                foreach (myThread k in threads)
                                {
                                    k.Stop();
                                }
                                enable.Active = true;
                                Report ot = new Report(ColichChain, CPU_load, Ram, FreeRam, VideoRam);
                                ot.ShowDialog();
                                return;
                            }
                            w += width1;
                        }
                        h += height1;
                    }
                }
                if (i % 2 == 0)
                {
                    int h = 0;
                    width1 = width1 / 2;
                    while (h < height)
                    {
                        int w = 0;
                        while (w < width)
                        {
                            if (enable.Active)
                            {
                                threads.Add(new myThread(w, h, width1, height1, enable));
                                ColichChain++;
                                if (hardware.GetProcessorInformation() == 100)
                                {
                                    string CPU_load = Convert.ToString(hardware.GetProcessorInformation());
                                    string Ram = hardware.GetMemoryInformation();
                                    string FreeRam = hardware.GetFreeMemoryInformation();
                                    string VideoRam = hardware.GetVideoRam();
                                    foreach (myThread k in threads)
                                    {
                                        k.Stop();
                                    }
                                    Report ot = new Report(ColichChain, CPU_load, Ram, FreeRam, VideoRam);
                                    ot.ShowDialog();
                                    return;
                                }
                            }
                            else
                            {
                                string CPU_load = Convert.ToString(hardware.GetProcessorInformation());
                                string Ram = hardware.GetMemoryInformation();
                                string FreeRam = hardware.GetFreeMemoryInformation();
                                string VideoRam = hardware.GetVideoRam();
                                foreach (myThread k in threads)
                                {
                                    k.Stop();
                                }
                                enable.Active = true;
                                Report ot = new Report(ColichChain, CPU_load, Ram, FreeRam, VideoRam);
                                ot.ShowDialog();
                                return;
                            }
                            w += width1;
                        }
                        h += height1;
                    }
                }
                i++;
            }
            string CPU_load1 = Convert.ToString(hardware.GetProcessorInformation());
            string Ram1 = hardware.GetMemoryInformation();
            string FreeRam1 = hardware.GetFreeMemoryInformation();
            string VideoRam1 = hardware.GetVideoRam();
            foreach (myThread k in threads)
            {
                k.Stop();
            }
            Report ot1 = new Report(ColichChain, CPU_load1, Ram1, FreeRam1, VideoRam1);
            ot1.ShowDialog();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            process.UpdateProcess();
        }

        void kh_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        void kh_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            kh.KeyDown += new KeyEventHandler(kh_KeyDown);
            kh.KeyUp += new KeyEventHandler(kh_KeyUp);
        }

        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            trafficWatcher.Stop();
            Application.Exit();
        }
    }
}
