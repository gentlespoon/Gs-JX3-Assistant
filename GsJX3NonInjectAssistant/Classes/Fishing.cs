using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GsJX3NonInjectAssistant
{
    class Fishing
    {
        // successful fishing
        private Point _coord_indicator_success = new Point(0, 0);
        private Color color_indicator_success = Color.FromRgb(0, 0, 0);
        
        // fishing status detection
        private Point _coord_indicator_regular_skillbar = new Point(0, 0);
        private Color color_indicator_regular_skillbar = Color.FromRgb(0, 0, 0);

        // button coordinates
        private Point _coord_button_start = new Point(0, 0);
        private Point _coord_button_end = new Point(0, 0);
        private Point _coord_button_fishing = new Point(0, 0);

        

    }
}
