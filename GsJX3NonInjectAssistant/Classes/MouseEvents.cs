using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;
using Gma.System.MouseKeyHook;

namespace GsJX3NonInjectAssistant
{
    class MouseEvents
    {
        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe(Common.CallBack callback)
        {
            if (null != callbackFunction)
            {
                throw new InvalidOperationException("Already subscribed to mouse events");
            }

            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            //m_GlobalHook.KeyPress += GlobalHookKeyPress;
            callbackFunction = callback;
        }

        
        Common.CallBack callbackFunction = null;

        //private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        //{
        //    Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        //}

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (null == callbackFunction)
            {
                throw new NullReferenceException("Callback is not set");
            }

            //Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);
            Point coordinates = new Point(e.X, e.Y);
            Unsubscribe();
            callbackFunction(coordinates);
            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            //m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }
    }
}
