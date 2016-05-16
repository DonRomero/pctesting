using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pctesting.TestHardware
{
    public partial class Report : Form
    {
        int _ColichChain;
        string _CPU;
        string _RAM;
        string _FreeRAM;
        string _VideoRAM;
        string _comp;
        string _user;
        HardwareInfo hardware = new HardwareInfo();

        public Report(int Colich, string CPU, string RAM, string FreeRAM, string VideoRam, string comp,string user)
        {
            InitializeComponent();
            _ColichChain = Colich;
            _CPU = CPU;
            _RAM = RAM;
            _FreeRAM = FreeRAM;
            _VideoRAM = (Convert.ToDouble(VideoRam)/1024).ToString();
            _comp = comp;
            _user = user;
        }
        private void Report_Load(object sender, EventArgs e)
        {
            DBService.DataServiceClient client = new DBService.DataServiceClient();
            textBox1.Text = Convert.ToString(_ColichChain);
            textBox2.Text = _CPU + "%";
            textBox3.Text = _RAM + "Кбайт";
            textBox4.Text = _FreeRAM + "Кбайт";
            textBox5.Text = _VideoRAM + "Байт";
            //client.SaveTestCharacteristic(DateTime.Now, _ColichChain, _RAM, _FreeRAM, _CPU, _VideoRAM, _comp, _user);
            if (_ColichChain < 20)
            {
                FinalReport.Text = "Ваш компьютер имеет очень низкую производительность.";
            }
            if (_ColichChain >= 20 && _ColichChain <= 65)
            {
                FinalReport.Text = "Ваш компьютер имеет низкую производительность.";
            }
            if (_ColichChain > 65 && _ColichChain <= 120)
            {
               FinalReport.Text = "Ваш компьютер имеет среднюю производительность.";
            }
            if (_ColichChain > 120 && _ColichChain <= 150)
            {
                FinalReport.Text = "Ваш компьютер имеет хорошую производительность.";
            }
            if (_ColichChain > 150)
            {
                FinalReport.Text = "Ваш компьютер имеет отличную производительность.";
            }

        }
    }
}
