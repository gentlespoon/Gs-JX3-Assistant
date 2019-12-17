using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Threading;
using GsJX3NonInjectAssistant.Classes.HID.Mouse;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace GsJX3NonInjectAssistant
{
    public static class Common
    {
            
        public readonly static System.Drawing.Point NullPoint = new System.Drawing.Point(0, 0);
        public readonly static System.Drawing.Color NullColor = System.Drawing.Color.Transparent;
        public readonly static System.Drawing.Size NullSize = new System.Drawing.Size(0, 0);

        public static System.Drawing.Point GetVirtualScreenResolution()
        {
            return new System.Drawing.Point(
                Convert.ToInt32(SystemParameters.VirtualScreenWidth),
                Convert.ToInt32(SystemParameters.VirtualScreenHeight)
            );
        }

        public static System.Windows.Media.Color ToMediaColor(this System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }


        public static int? ParsePositiveInt(string text)
        {
            try
            {
                int n = Convert.ToInt32(text);
                return n;
            }
            catch
            { /* suppress error */ }
            return null;
        }

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            MemoryStream memStream = new MemoryStream();
            bitmap.Save(memStream, ImageFormat.Jpeg);
            return memStream.ToArray();
        }

    }
}
