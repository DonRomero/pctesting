using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace pctesting.TestHardware
{
    public class myThread
    {
        Thread thread;
        private int x;
        private int y;
        private int width;
        private int heigth;
        CheckEnable enable;
        public myThread(int _x, int _y, int _width, int _heigth, CheckEnable _enable)
        {
            x = _x;
            y = _y;
            width = _width;
            heigth = _heigth;
            enable = _enable;
            thread = new Thread(this.Show_teapot);
            thread.Start();
        }
        public void Stop()
        {
            thread.Abort();
        }
        private void Show_teapot()
        {
            try
            {
                Teapot form = new Teapot(enable);
                form.Text = "teapot";
                form.Width = width;
                form.Height = heigth;
                form.InitD3D();
                form.SetDesktopBounds(x, y, width, heigth);
                form.Show();
                while (form.Created)
                {
                    form.Render();
                    Application.DoEvents();
                }
            }
            catch(Exception e)
            {
                enable.Active = false;
                return;
            }
        }
    }
}

