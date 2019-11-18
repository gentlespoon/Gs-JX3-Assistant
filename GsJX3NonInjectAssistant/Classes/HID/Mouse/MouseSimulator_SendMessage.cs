using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace GsJX3NonInjectAssistant.Classes.HID.Mouse
{
    class MouseSimulator_SendMessage : IMouseSimulator
    {
        public void Click(Point point, int mouseButton = 1, bool dblClick = false)
        {
            throw new NotImplementedException();
        }
    }
}
