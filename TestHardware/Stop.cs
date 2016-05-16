using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pctesting.TestHardware
{
    public partial class Stop : Form
    {
        string user;
        string comp;
        public Stop(string userName, string compMAC)
        {
            InitializeComponent();
            user = userName;
            comp = compMAC;

        }
        CheckEnable enable = new CheckEnable();
        private void StopTest_Click(object sender, EventArgs e)
        {
            enable.Active = false;
        }

        private void backgroundTest_DoWork(object sender, DoWorkEventArgs e)
        {
            TestHardware.Algorithm.BeginTest(comp, "", enable);
        }

        private void Stop_Load(object sender, EventArgs e)
        {
            backgroundTest.RunWorkerAsync();
        }

        private void backgroundTest_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
