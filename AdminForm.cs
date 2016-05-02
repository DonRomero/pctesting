using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pctesting.DBService;

namespace pctesting
{
    public partial class AdminForm : Form
    {
        DBService.DataServiceClient client = new DataServiceClient();
        public AdminForm(string userName, string compName)
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void pctestingIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AdminForm_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Close();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            TestHardware.Algorithm.BeginTest();
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            new AddUserForm().ShowDialog();
        }
    }
}
