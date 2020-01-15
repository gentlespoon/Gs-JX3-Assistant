using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3NIA.Classes.HID.Display
{
    public class ScreenCaptureConfiguration
    {
        public Point TopLeft = Common.NullPoint;
        public Point BottomRight = Common.NullPoint;
        public Size Size
        {
            get
            {
                if (TopLeft != BottomRight &&
                    Common.NullPoint != TopLeft &&
                    Common.NullPoint != BottomRight &&
                    BottomRight.Y > TopLeft.Y &&
                    BottomRight.X > TopLeft.X
                    )
                {
                    return new Size(BottomRight.X - TopLeft.X, BottomRight.Y - TopLeft.Y);
                }
                return Common.NullSize;
            }
        }
    }
}
