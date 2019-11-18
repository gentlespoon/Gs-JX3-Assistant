using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3NonInjectAssistant.Classes.HID.Mouse
{
    public interface IMouseReader
    {
        delegate void GetCursorPositionCallBack(System.Drawing.Point p, int MouseButton);
        void GetCursorPosition(GetCursorPositionCallBack callback);
    }
}
