using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pctesting
{
    public partial class UserActions : Form
    {
        DBService.DataServiceClient client = new DBService.DataServiceClient();
        public UserActions()
        {
            InitializeComponent();
        }

        private void UserActions_Load(object sender, EventArgs e)
        {
            UsersBox.DataSource = client.getUsers().Where(us=>us!="admin").ToList();

        }

        private void CreateReportButton_Click(object sender, EventArgs e)
        {
            FrameInfo.Rows.Clear();
            FrameInfo.ColumnCount = 5;
            FrameInfo.Columns[0].HeaderText = "Имя пользователя";
            FrameInfo.Columns[1].HeaderText = "Тип деятельности";
            FrameInfo.Columns[2].HeaderText = "Общее время работы";
            FrameInfo.Columns[3].HeaderText = "Время начала";
            FrameInfo.Columns[4].HeaderText = "Время конца";
            List<UserInfoFrame> Frame = new List<UserInfoFrame>();
            Frame = GetFrames.DivideOnFrame(UsersBox.Text);          
            try
            {
                FrameInfo.RowCount = Frame.Count();
            }
            catch(Exception ex)
            {
                MessageBox.Show("На данного пользователя информация не была собрана");
                return;
            }
            for (int i = 0; i < Frame.Count(); i++)
            {
                FrameInfo.Rows[i].Cells[0].Value = Frame[i].UserName;
                FrameInfo.Rows[i].Cells[1].Value = Frame[i].TypeActivity;
                FrameInfo.Rows[i].Cells[2].Value = new TimeSpan(Convert.ToInt64(Frame[i].GeneralTime)).ToString();
                FrameInfo.Rows[i].Cells[3].Value = Frame[i].BeginTime;
                FrameInfo.Rows[i].Cells[4].Value = Frame[i].EndTime;

            }
        }

        private void GeneralReport_Click(object sender, EventArgs e)
        {
            List<GeneralFrame> GF = GetFrames.DivideOnGeneralFrame(UsersBox.Text);
            FrameInfo.Rows.Clear();
            FrameInfo.ColumnCount = 3;
            FrameInfo.RowCount = 2;
            FrameInfo.Columns[0].HeaderText = "Имя пользователя";
            FrameInfo.Columns[1].HeaderText = "Тип деятельности";
            FrameInfo.Columns[2].HeaderText = "Процент от общего времени";
            FrameInfo.Rows[0].Cells[0].Value = GF[0].UserName;
            FrameInfo.Rows[0].Cells[1].Value = GF[0].TypeActivity;
            FrameInfo.Rows[0].Cells[2].Value = GF[0].Percent;
            FrameInfo.Rows[1].Cells[0].Value = GF[1].UserName;
            FrameInfo.Rows[1].Cells[1].Value = GF[1].TypeActivity;
            FrameInfo.Rows[1].Cells[2].Value = GF[1].Percent;
        }

    }
}
