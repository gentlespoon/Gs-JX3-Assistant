using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DColor = System.Drawing.Color;

namespace GsJX3NonInjectAssistant.Classes.HID.Display
{
    public interface IDisplayHelper
    {
        DColor GetColorAt(Point point);
        Bitmap CaptureScreen(ScreenCaptureConfiguration scConf);
    }
}
