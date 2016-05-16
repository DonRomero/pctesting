using System;
using System.Windows.Forms;
using pctesting.DBService;
using System.Diagnostics;
using System.IO;

namespace pctesting
{
    public partial class AdminForm : Form
    {
        DBService.DataServiceClient client = new DataServiceClient();
        string diskLetter = "";
        string comp;
        public AdminForm(string userName, string compMAC, string diskLetter)
        {
            InitializeComponent();
            this.diskLetter = diskLetter;
            this.comp = compMAC;
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
            TestHardware.Algorithm.BeginTest(comp,"");
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
            try
            {
                client.makeReport();
                MessageBox.Show("Отчёты созданы.", "Успешно");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Возникла ошибка при создании отчётов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backupButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(diskLetter + @"pctesting") || !File.Exists(diskLetter + @"pctesting\mydb.sqlite"))
            {
                MessageBox.Show("Файлы базы данных отсутствуют!", "Отсутствует файл", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(diskLetter + @"pctesting\backup");
                    File.Copy(diskLetter + @"\pctesting\mydb.sqlite", diskLetter + @"\pctesting\backup\mydb-backup_" + DateTime.Now.ToString().Replace('.', '-').Replace(' ', '-').Replace(':', '-') + ".sqlite");
                    MessageBox.Show("База данных успешно сохранена.", "Бекап");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла ошибка при сохранении базы данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
