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

using System.Timers;

using GsJX3NonInjectAssistant.Fishing;

using YariControl.RealCursorPosition;
using System.Windows.Media;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;
using System.Threading;

namespace GsJX3NonInjectAssistant.Views.Fishing
{
    /// <summary>
    /// Interaction logic for FishingPage.xaml
    /// </summary>
    public partial class Fishing : Page
    {
        private FishingController fishingController = new FishingController();
        private System.Timers.Timer timer_pollStatus = new System.Timers.Timer(500);

        public Fishing()
        {
            InitializeComponent();

            timer_pollStatus.Elapsed += Timer_PollStatus_Ticker;
            timer_pollStatus.AutoReset = true;
            timer_pollStatus.Start();
        }


        ~Fishing()
        {
            timer_pollStatus.Stop();
            timer_pollStatus.Dispose();
        }

        private void Timer_PollStatus_Ticker(Object source, ElapsedEventArgs e)
        {
            Task.Run(UpdateUI);
        }

        public void UpdateUI()
        {
            Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                var fC = fishingController;
                var state = fC.State;
                label_loopCounter.Content = $"{fC.CounterSuccess} / {fC.CounterTotal}";
                button_start.IsEnabled = state.RequiredCoords && !state.Running;
                button_stop.IsEnabled = state.RequiredCoords && state.Running;
                label_progressMonitor_requiredCoordsSet.Content = state.RequiredCoords ? "√" : "x";
                label_progressMonitor_fishingMode.Content = state.Running ? (state.OptionalCoords ? (state.FishingMode ? "√" : "x") : "-") : "";
                label_progressMonitor_fishingStarted.Content = state.Running ? (state.Started ? "√" : "") : "";
                label_progressMonitor_fishingSuccess.Content = state.Running ? (state.Started ? (!state.Stopped ? fC.timer_timeout_tick.ToString() : "√") : "") : "";
                label_progressMonitor_fishingStopped.Content = state.Running ? (state.Stopped ? fC.timer_waitForPickup_tick.ToString() : "") : "";
            }));

        }




        private void button_setCoordinates_skillBar_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_skillBar.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point, string mouseButton) => {
                fishingController.ACDS.RegularSkillBar.Coordinates = point;
                DColor color = ScreenPixelColor.GetPixelColor(point);
                fishingController.ACDS.RegularSkillBar.PixelColor = color;
                label_skillBar.Foreground = new SolidColorBrush(Common.ToMediaColor(DColor.FromArgb(color.ToArgb() ^ 0xffffff)));
                label_skillBar.Content = $"{mouseButton} {point.ToString()}";
                label_skillBar.Background = new SolidColorBrush(Common.ToMediaColor(color));
                Console.WriteLine($"Got Regular_SkillBar: {point.ToString()}, {color.ToString()}");
                fishingController.VerifyACDS();
                button_setCoordinates_skillBar.IsEnabled = true;
            });
        }
        
        private void button_setCoordinates_fishingMode_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_fishingMode.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point, string mouseButton) => {
                fishingController.ACDS.EnterFishingMode.Coordinates = point;
                fishingController.ACDS.EnterFishingMode.MouseAction = mouseButton;
                label_enterFishing.Foreground = new SolidColorBrush(MColor.FromRgb(0,0,0));
                label_enterFishing.Content = $"{mouseButton} {point.ToString()}";
                Console.WriteLine($"Got FishingMode_Button: {point.ToString()}");
                fishingController.VerifyACDS();
                button_setCoordinates_fishingMode.IsEnabled = true;
            });
        }

        private void button_setCoordinates_startFishing_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_startFishing.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point, string mouseButton) => {
                fishingController.ACDS.StartFishing.Coordinates = point;
                fishingController.ACDS.StartFishing.MouseAction = mouseButton;
                label_startFishing.Foreground = new SolidColorBrush(MColor.FromRgb(0, 0, 0));
                label_startFishing.Content = $"{mouseButton} {point.ToString()}";
                Console.WriteLine($"Got StartFishing_Button: {point.ToString()}");
                fishingController.VerifyACDS();
                button_setCoordinates_startFishing.IsEnabled = true;
            });
        }

        private void button_setCoordinates_successIndicator_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_successIndicator.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point, string mouseButton) => {
                fishingController.ACDS.SuccessIndicator.Coordinates = point;
                DColor color = ScreenPixelColor.GetPixelColor(point);
                fishingController.ACDS.SuccessIndicator.PixelColor = color;
                label_success.Foreground = new SolidColorBrush(Common.ToMediaColor(DColor.FromArgb(color.ToArgb() ^ 0xffffff)));
                label_success.Content = $"{mouseButton} {point.ToString()}";
                label_success.Background = new SolidColorBrush(Common.ToMediaColor(color));
                Console.WriteLine($"Got Success_Indicator: {point.ToString()}, {color.ToString()}");
                fishingController.VerifyACDS();
                button_setCoordinates_successIndicator.IsEnabled = true;
            });
        }

        private void button_setCoordinates_endFishing_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_endFishing.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point, string mouseButton) => {
                fishingController.ACDS.StopFishing.Coordinates = point;
                fishingController.ACDS.StopFishing.MouseAction = mouseButton;
                label_endFishing.Foreground = new SolidColorBrush(MColor.FromRgb(0, 0, 0));
                label_endFishing.Content = $"{mouseButton} {point.ToString()}";
                Console.WriteLine($"Got StopFishing_Button: {point.ToString()}");
                fishingController.VerifyACDS();
                button_setCoordinates_endFishing.IsEnabled = true;
            });
        }

        private void button_setCoordinates_revive_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_revive.IsEnabled = false;
            Common.GetMouseClickCoordinate((System.Drawing.Point point, string mouseButton) =>
            {
                fishingController.ACDS.Revive.Coordinates = point;
                fishingController.ACDS.Revive.MouseAction = mouseButton;
                DColor color = ScreenPixelColor.GetPixelColor(point);
                fishingController.ACDS.Revive.PixelColor = color;
                label_revive.Foreground = new SolidColorBrush(Common.ToMediaColor(DColor.FromArgb(color.ToArgb() ^ 0xffffff)));
                label_revive.Content = $"{mouseButton} {point.ToString()}";
                label_revive.Background = new SolidColorBrush(Common.ToMediaColor(color));
                Console.WriteLine($"Got Revive_Button: {point.ToString()}, {color.ToString()}");
                fishingController.VerifyACDS();
                button_setCoordinates_revive.IsEnabled = true;
            });
        }

        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            fishingController.Start();
        }

        private void button_stop_Click(object sender, RoutedEventArgs e)
        {
            fishingController.Stop();
        }

        private void textBox_timeout_LostFocus(object sender, RoutedEventArgs e)
        {
            string txt = textBox_fishingTimeout.Text;
            if (txt.Trim() == "")
            {
                textBox_fishingTimeout.Text = fishingController.timer_timeout.ToString();
                return;
            }
            int? n = Common.ParsePositiveInt(txt);
            if (null != n && n > 0)
                fishingController.timer_timeout = (int)n;
            else
                MessageBox.Show($"{txt} 不是有效的数字");
        }

        private void textBox_pickupDelay_LostFocus(object sender, RoutedEventArgs e)
        {
            string txt = textBox_pickupDelay.Text;
            if (txt.Trim() == "")
            {
                textBox_pickupDelay.Text = fishingController.timer_waitForPickup.ToString();
                return;
            }
            int? n = Common.ParsePositiveInt(txt);
            if (null != n && n > 0)
                fishingController.timer_waitForPickup = (int)n;
            else
                MessageBox.Show($"{txt} 不是有效的数字");
        }

        private void textBox_pickupDelay_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox_pickupDelay.SelectionStart = 0;
            textBox_pickupDelay.SelectionLength = textBox_pickupDelay.Text.Length;
        }

        private void textBox_timeout_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox_fishingTimeout.SelectionStart = 0;
            textBox_fishingTimeout.SelectionLength = textBox_fishingTimeout.Text.Length;
        }

        private void button_resetStatistics_Click(object sender, RoutedEventArgs e)
        {
            fishingController.ResetStatistics();
        }
    }
}
