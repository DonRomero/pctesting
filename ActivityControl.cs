using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace pctesting
{
    class ActivityControl
    {
        private IKeyboardMouseEvents m_GlobalHook;
        private DateTime StartWorkTime;

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
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
            client.SaveActivityToDB(DateTime.Now-StartWorkTime,);
        }
    }
}
