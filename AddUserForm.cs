using System;
using System.Windows.Forms;
using pctesting.DBService;

namespace pctesting
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DBService.DataServiceClient client = new DataServiceClient();
            if (userNameTextBox.Text.Length > 0 && userPasswordTextBox.Text.Length > 0 && client.addUser(userNameTextBox.Text, userPasswordTextBox.Text))
            {
                MessageBox.Show("Пользователь успешно добавлен", "Пользователь добавлен");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Возникла ошибка добавления пользователя!\nВозможно, пользователь с таким именем уже существует.", "Ошибка добавления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
