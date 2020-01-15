using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;
using Gma.System.MouseKeyHook;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;

namespace GsJX3NIA.Classes.HID.Mouse
{
    class MouseReader_MouseKeyHook : IMouseReader
    {
        private IKeyboardMouseEvents m_GlobalHook;
        IMouseReader.GetCursorPositionCallBack callbackFunction = null;

        public delegate void GetCursorPositionCallBack(Point p, int MouseButton);

        public void GetCursorPosition(IMouseReader.GetCursorPositionCallBack callback)
        {
            callbackFunction = callback;
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (null == callbackFunction)
            {
                throw new NullReferenceException("Callback is not set");
            }

            Point coordinates = new Point(e.X, e.Y);
            int mouseButton;
            switch (e.Button)
            {
                default:                    mouseButton = 0;     break;
                case MouseButtons.Left:     mouseButton = 1;    break;
                case MouseButtons.Right:    mouseButton = 2;    break;
                case MouseButtons.Middle:   mouseButton = 3;    break;
            }
            callbackFunction(coordinates, mouseButton);
            Unsubscribe();
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.Dispose();
            callbackFunction = null;
        }

    }
}
