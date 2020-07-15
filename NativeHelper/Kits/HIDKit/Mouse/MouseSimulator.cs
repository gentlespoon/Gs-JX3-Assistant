using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GsJX3AssistantNativeHelper.Kits {
    class MouseSimulator_MouseEvent : IMouseSimulator
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);


        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;

        public void Click(Point point, int mouseButton = 1, bool dblClick = false)
        {
            // JX3 does not like SendMessage() API...
            // So fall back to the deprecated mouse_event API.

            // Console.WriteLine($"{mouseButton} Click at {point.ToString()}");

            // for some reason this coordinates does not work
            // mouse movement relies on the Cursor.Position assignment.
            uint X = (uint)point.X;
            uint Y = (uint)point.Y;

            Point currentMousePosition = Cursor.Position;
            Cursor.Position = point;

            switch (mouseButton)
            {
                case 1: mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0); break;
                case 2: mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0); break;
                case 3: mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, X, Y, 0, 0); break;
                default: break;
            }

            if (dblClick)
            {
                Thread.Sleep(20);
                switch (mouseButton)
                {
                    case 1: mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0); break;
                    case 2: mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0); break;
                    case 3: mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, X, Y, 0, 0); break;
                    default: break;
                }
            }

            // reset mouse position
            Cursor.Position = currentMousePosition;
        }
    }
}