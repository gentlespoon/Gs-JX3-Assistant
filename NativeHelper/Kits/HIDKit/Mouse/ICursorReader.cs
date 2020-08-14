using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3AssistantNativeHelper.Kits.HIDKits.Mouse
{
    public interface ICursorReader
    {
        delegate void GetCursorPositionCallBack(System.Drawing.Point p, int MouseButton);
        void GetCursorPosition(GetCursorPositionCallBack callback);
    }
}
