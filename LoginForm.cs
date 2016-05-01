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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            DBService.DataServiceClient client = new DataServiceClient();
             string IP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0].ToString();
            if (loginTextBox.Text.Equals("") | passwordTextBox.Text.Equals(""))
                MessageBox.Show("Введите корректные данные!", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                try
                {
                    switch (client.login(loginTextBox.Text, passwordTextBox.Text, IP))
                    {
                        case "user":
                            new UserForm(loginTextBox.Text).Show();
                            this.Hide();
                            break;
                        case "admin":
                            new AdminForm(loginTextBox.Text).Show();
                            this.Hide();
                            break;
                        default:
                            MessageBox.Show("Неверный логин или пароль", "В доступе отказано!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Возникла ошибка подключения!\nПроверьте работоспособность сервера.", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
    }
}
