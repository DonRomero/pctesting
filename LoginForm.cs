using System;
using System.Windows.Forms;
using pctesting.DBService;
using System.IO;

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
            string compName;// = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0].ToString();
            if (!Directory.Exists(diskLetter + @"pctesting"))
                Directory.CreateDirectory(diskLetter + @"pctesting");
            if (!File.Exists(diskLetter + @"pctesting\guid.txt"))
            { 
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(diskLetter + @"pctesting\guid.txt"))
                {
                    compName = Guid.NewGuid().ToString();
                    file.WriteLine(compName);
                }
            }
            else
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(diskLetter + @"pctesting\guid.txt"))
                {
                    compName = file.ReadLine();
                }
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
