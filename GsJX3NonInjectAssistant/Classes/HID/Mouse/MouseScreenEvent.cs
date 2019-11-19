using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GsJX3NonInjectAssistant.Classes.HID
{
    public class MouseScreenEvent
    {
        public Point Coordinates = Common.NullPoint;
        public Color PixelColor = Common.NullColor;
        public int MouseAction = 0;
    }
}
