using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DColor = System.Drawing.Color;

namespace GsJX3AssistantNativeHelper.Kits
{
    public interface IDisplayHelper
    {
        DColor GetColorAt(Point point);
    }
}