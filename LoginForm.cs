using System;
using System.Windows.Forms;
using pctesting.DBService;
using System.IO;
using System.Management;

namespace pctesting
{
    public partial class LoginForm : Form
    {
        string diskLetter = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            DBService.DataServiceClient client = new DataServiceClient();
            string compName = "";// = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0].ToString();
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapter");
            ManagementObjectCollection col = mc.GetInstances();
            foreach (ManagementObject mo in col)
            {
                string macAddr = mo["MACAddress"] as string;
                if (macAddr != null && macAddr.Trim() != "")
                    compName = macAddr;
            }
            if (loginTextBox.Text.Equals("") | passwordTextBox.Text.Equals(""))
                MessageBox.Show("Введите корректные данные!", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                try
                {
                    switch (client.login(loginTextBox.Text, passwordTextBox.Text, compName))
                    {
                        case "user":
                            new UserForm(loginTextBox.Text, compName).Show();
                            this.Hide();
                            break;
                        case "admin":
                            new AdminForm(loginTextBox.Text, compName).Show();
                            this.Hide();
                            break;
                        default:
                            MessageBox.Show("Неверный логин или пароль", "В доступе отказано!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла ошибка подключения!\nПроверьте работоспособность сервера.", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
