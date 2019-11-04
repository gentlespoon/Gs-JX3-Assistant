using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Navigation;
using System.Windows.Shapes;

using GsJX3NonInjectAssistant.Fishing;

using YariControl.RealCursorPosition;
using System.Windows.Media;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;

namespace GsJX3NonInjectAssistant.Views.Fishing
{
    /// <summary>
    /// Interaction logic for FishingPage.xaml
    /// </summary>
    public partial class Fishing : Page
    {
        private FishingController fishingController = new FishingController();

        public Fishing()
        {
            InitializeComponent();
        }


        private void button_setCoordinates_skillBar_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_skillBar.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point) => {
                fishingController.Coord_Indicator_Regular_SkillBar = point;
                DColor color = ScreenPixelColor.GetPixelColor(point);
                fishingController.Color_Indicator_Regular_SkillBar = color;
                label_skillBar.Foreground = new SolidColorBrush(Common.ToMediaColor(DColor.FromArgb(color.ToArgb() ^ 0xffffff)));
                label_skillBar.Content = point.ToString();
                label_skillBar.Background = new SolidColorBrush(Common.ToMediaColor(color));
                Console.WriteLine($"Got Regular_SkillBar: {point.ToString()}, {color.ToString()}");
                button_setCoordinates_skillBar.IsEnabled = true;
                button_start.IsEnabled = fishingController.CanStartFishing;
            });
        }

        private void button_setCoordinates_fishingMode_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_fishingMode.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point) => {
                fishingController.Coord_Button_EnterFishingMode = point;
                label_enterFishing.Foreground = new SolidColorBrush(MColor.FromRgb(0,0,0));
                label_enterFishing.Content = point.ToString();
                Console.WriteLine($"Got FishingMode_Button: {point.ToString()}");
                button_setCoordinates_fishingMode.IsEnabled = true;
                button_start.IsEnabled = fishingController.CanStartFishing;
            });
        }

        private void button_setCoordinates_startFishing_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_startFishing.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point) => {
                fishingController.Coord_Button_StartFishing = point;
                label_startFishing.Foreground = new SolidColorBrush(MColor.FromRgb(0, 0, 0));
                label_startFishing.Content = point.ToString();
                Console.WriteLine($"Got StartFishing_Button: {point.ToString()}");
                button_setCoordinates_startFishing.IsEnabled = true;
                button_start.IsEnabled = fishingController.CanStartFishing;
            });
        }

        private void button_setCoordinates_successIndicator_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_successIndicator.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point) => {
                fishingController.Coord_Indicator_Success = point;
                DColor color = ScreenPixelColor.GetPixelColor(point);
                fishingController.Color_Indicator_Success = color;
                label_success.Foreground = new SolidColorBrush(Common.ToMediaColor(DColor.FromArgb(color.ToArgb() ^ 0xffffff)));
                label_success.Content = point.ToString();
                label_success.Background = new SolidColorBrush(Common.ToMediaColor(color));
                Console.WriteLine($"Got Success_Indicator: {point.ToString()}, {color.ToString()}");
                button_setCoordinates_successIndicator.IsEnabled = true;
                button_start.IsEnabled = fishingController.CanStartFishing;
            });
        }

        private void button_setCoordinates_endFishing_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_endFishing.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point) => {
                fishingController.Coord_Button_EndFishing = point;
                label_endFishing.Foreground = new SolidColorBrush(MColor.FromRgb(0, 0, 0));
                label_endFishing.Content = point.ToString();
                Console.WriteLine($"Got StopFishing_Button: {point.ToString()}");
                button_setCoordinates_endFishing.IsEnabled = true;
                button_start.IsEnabled = fishingController.CanStartFishing;
            });
        }

        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            fishingController.Start();
            button_start.IsEnabled = fishingController.CanStartFishing;
            button_stop.IsEnabled = fishingController.CanStopFishing;
        }

        private void button_stop_Click(object sender, RoutedEventArgs e)
        {
            fishingController.Stop();
            button_start.IsEnabled = fishingController.CanStartFishing;
            button_stop.IsEnabled = fishingController.CanStopFishing;
        }
    }
}
