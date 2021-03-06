﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using DColor = System.Drawing.Color;
using System.Runtime.InteropServices;
using System.IO;

namespace GsJX3NIA.Classes.HID.Display
{
    

    class DisplayHelper_GDI : IDisplayHelper
    {

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);


        Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        public DColor GetColorAt(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            return screenPixel.GetPixel(0, 0);
        }

        public Bitmap CaptureScreen(ScreenCaptureConfiguration scConf)
        {
            try
            {
                Bitmap bitmap = new Bitmap(scConf.Size.Width, scConf.Size.Height);
                using Graphics g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(scConf.TopLeft.X, scConf.TopLeft.Y, 0, 0, scConf.Size);
                return bitmap;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturing screen");
                Console.WriteLine(ex.Message);
                return new Bitmap(1, 1);
            }
        }


    }
}
