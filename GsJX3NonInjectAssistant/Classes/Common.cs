using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Threading;
using System.Windows.Media;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;

namespace GsJX3NonInjectAssistant
{
    public static class Common
    {


        public delegate void CallBack(System.Drawing.Point p);

        public static System.Drawing.Point GetVirtualScreenResolution()
        {
            return new System.Drawing.Point(
                Convert.ToInt32(SystemParameters.VirtualScreenWidth),
                Convert.ToInt32(SystemParameters.VirtualScreenHeight)
            );
        }

        public static void GetMouseClickCoordinate(CallBack callback)
        {
            MouseEvents me = new MouseEvents();
            me.Subscribe(callback);
        }

        public static MColor ToMediaColor(this DColor color)
        {
            return MColor.FromArgb(color.A, color.R, color.G, color.B);
        }


    }
}
