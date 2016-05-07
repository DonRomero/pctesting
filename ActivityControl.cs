using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace pctesting
{
    class ActivityControl
    {
        private IKeyboardMouseEvents m_GlobalHook;
        private DateTime StartWorkTime;
        private DateTime LastInput;
        private TimeSpan PassiveTime;

        string user;
        string comp;
        
        public ActivityControl(string user, string comp)
        {
            this.user=user;
            this.comp=comp;
        }
        public void Subscribe()
        {
            StartWorkTime = DateTime.Now;
            LastInput = StartWorkTime;
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {          
            PassiveTime += (DateTime.Now - LastInput);
            LastInput = DateTime.Now;
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            PassiveTime += (DateTime.Now - LastInput);
            LastInput = DateTime.Now;
            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        public void Unsubscribe()
        {
            DBService.DataServiceClient client = new DBService.DataServiceClient();
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;
            //It is recommened to dispose it
            m_GlobalHook.Dispose();
            client.SaveActivityToDB((DateTime.Now - StartWorkTime), (DateTime.Now - StartWorkTime)-PassiveTime,PassiveTime,comp,user);
        }
    }
}
