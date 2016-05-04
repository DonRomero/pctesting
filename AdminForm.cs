using System;
using System.Windows.Forms;
using pctesting.DBService;
using System.Diagnostics;

namespace pctesting
{
    public partial class AdminForm : Form
    {
        DBService.DataServiceClient client = new DataServiceClient();
        string diskLetter = "";
        public AdminForm(string userName, string compMAC, string diskLetter)
        {
            InitializeComponent();
            this.diskLetter = diskLetter;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            pctestingIcon.Visible = false;
        }

        private void pctestingIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            pctestingIcon.Visible = false;
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
                pctestingIcon.Visible = true;
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
                pctestingIcon.Visible = true;
            }
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            new AddUserForm().ShowDialog();
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            client.makeReport();
            Process.Start(diskLetter+@"pctesting\report\");
        }
    }
}
