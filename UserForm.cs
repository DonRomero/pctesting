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
        public UserForm(string userName, string compName)
        {
            InitializeComponent();
            fileWatcher = new FileManager(userName, compName);
            trafficWatcher = new TrafficManager(userName, compName);
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

        private void pctetingIcon_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void testButton_Click(object sender, EventArgs e)
        {
            Algorithm.BeginTest();
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
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            trafficWatcher.Stop();
            new LoginForm().Show();
            this.Close();
        }
    }
}
