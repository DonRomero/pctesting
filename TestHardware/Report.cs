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
        HardwareInfo hardware = new HardwareInfo();

        public Report(int Colich, string CPU, string RAM, string FreeRAM, string VideoRam)
        {
            InitializeComponent();
            _ColichChain = Colich;
            _CPU = CPU;
            _RAM = RAM;
            _FreeRAM = FreeRAM;
            _VideoRAM = VideoRam;
        }
        private void Report_Load(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(_ColichChain);
            textBox2.Text = _CPU + "%";
            textBox3.Text = _RAM + "Кбайт";
            textBox4.Text = _FreeRAM + "Кбайт";
            textBox5.Text = _VideoRAM + "Байт";
            if (_ColichChain < 20)
            {
                label5.Text = "Ваш компьютер имеет очень низкую производительность производительность.";
            }
            if (_ColichChain >= 20 && _ColichChain <= 65)
            {
                label5.Text = "Ваш компьютер имеет низкую производительность производительность.";
            }
            if (_ColichChain > 65 && _ColichChain <= 120)
            {
                label5.Text = "Ваш компьютер имеет среднюю производительность производительность.";
            }
            if (_ColichChain > 120 && _ColichChain <= 150)
            {
                label5.Text = "Ваш компьютер имеет хорошую производительность производительность.";
            }
            if (_ColichChain > 150)
            {
                label5.Text = "Ваш компьютер имеет отличную производительность производительность.";
            }
        }
    }
}
