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
        public AdminForm(string userName)
        {
            InitializeComponent();
            getUsers();
        }

        private void getUsers()
        {
            string[] users = client.getUsers();
            reportComboBox.DataSource = users;
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            if (reportComboBox.Items.Count == 0)
            {
                MessageBox.Show("В системе отсутствуют пользователи!", "Нет пользователей", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (client.makeReport(reportComboBox.SelectedItem.ToString()))
                {
                    MessageBox.Show("Отчёты успешно сохранены.", "Отчёты сохранены");
                }
                else
                {
                    MessageBox.Show("Возникла ошибка сохранения отчётов!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            new AddUserForm().ShowDialog();
            getUsers();
        }
    }
}
