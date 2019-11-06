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

namespace GsJX3NonInjectAssistant
{

    internal struct MouseInput
    {
        public int X;
        public int Y;
        public uint MouseData;
        public uint Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }

    internal struct Input
    {
        public int Type;
        public MouseInput MouseInput;
    }

    class MouseEvents
    {
        private IKeyboardMouseEvents m_GlobalHook;
        Common.GetMouseEventCallBack callbackFunction = null;

        public void Subscribe(Common.GetMouseEventCallBack callback)
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

            Point coordinates = new Point(e.X, e.Y);
            Unsubscribe();
            string mouseButton;
            switch (e.Button)
            {
                default:                    mouseButton = "";     break;
                case MouseButtons.Left:     mouseButton = "L";    break;
                case MouseButtons.Right:    mouseButton = "R";    break;
                case MouseButtons.Middle:   mouseButton = "M";    break;
            }
            callbackFunction(coordinates, mouseButton);
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            //m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;

        public static void SimulateMouseClick(Point point, string mouseButton, bool doubleClick = false)
        {
            // JX3 does not like SendMessage() API...
            // So fall back to the deprecated mouse_event API.

            Console.WriteLine($"{mouseButton} Click at {point.ToString()}");

            // for some reason this coordinates does not work
            // mouse movement relies on the Cursor.Position assignment.
            uint X = (uint)point.X;
            uint Y = (uint)point.Y;

            Point currentMousePosition = Cursor.Position;
            Cursor.Position = point;

            switch (mouseButton)
            {
                case "L": mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0); break;
                case "R": mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0); break;
                case "M": mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, X, Y, 0, 0); break;
                default: break;
            }

            if (doubleClick)
            {
                switch (mouseButton)
                {
                    case "L": mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0); break;
                    case "R": mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0); break;
                    case "M": mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, X, Y, 0, 0); break;
                    default: break;
                }
            }

            // reset mouse position
            Cursor.Position = currentMousePosition;
        }


    }
}
