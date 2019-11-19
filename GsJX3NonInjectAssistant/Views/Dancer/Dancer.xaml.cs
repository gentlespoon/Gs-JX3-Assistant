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

using System.Windows.Media;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;
using System.Threading;

using GsJX3NonInjectAssistant;
using GsJX3NonInjectAssistant.Classes.HID.Display;
using GsJX3NonInjectAssistant.Classes.HID.Mouse;
using GsJX3NonInjectAssistant.Classes.Features.Dancer;

namespace GsJX3NonInjectAssistant.Views.Dancer
{
    /// <summary>
    /// Interaction logic for FishingPage.xaml
    /// </summary>
    public partial class Dancer : Page
    {
        private DancerController dancerController;
        private System.Timers.Timer timer_pollStatus = new System.Timers.Timer(500);
        private IDisplayHelper displayHelper;
        private IMouseReader mouseReader;
        private IMouseSimulator mouseSimulator;

        public Dancer()
        {
            InitializeComponent();
            displayHelper = new DisplayHelper_GDI();
            mouseReader = new MouseReader_MouseKeyHook();
            mouseSimulator = new MouseSimulator_MouseEvent();

            dancerController = new DancerController(displayHelper, mouseReader, mouseSimulator);


            timer_pollStatus.Elapsed += Timer_PollStatus_Ticker;
            timer_pollStatus.AutoReset = true;
            timer_pollStatus.Start();
        }


        ~Dancer()
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
                var dC = dancerController;
                var state = dC.State;
                label_loopCounter.Content = dC.CounterTotal.ToString();
                button_start.IsEnabled = state.CanStartDancing && state.DancingMonitor && !state.Running;
                button_stop.IsEnabled = state.CanStartDancing && state.DancingMonitor && state.Running;

                if (state.Running)
                {
                    label_progressMonitor_dancingCountDown.Content = state.CanStopDancing ? (state.Started ? dC.timer_timeout_tick.ToString() : "x") : "√";
                }
                else
                {
                    label_progressMonitor_dancingCountDown.Content = "x";
                }

            }));

        }




        private void button_setCoordinates_skillBar_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_skillBar.IsEnabled = false;

            mouseReader.GetCursorPosition((System.Drawing.Point point, int mouseButton) => {
                dancerController.ACDS.RegularSkillBar.Coordinates = point;
                DColor color = displayHelper.GetColorAt(point);
                dancerController.ACDS.RegularSkillBar.PixelColor = color;
                label_skillBar.Foreground = new SolidColorBrush(Common.ToMediaColor(DColor.FromArgb(color.ToArgb() ^ 0xffffff)));
                label_skillBar.Content = $"{mouseButton} {point.ToString()}";
                label_skillBar.Background = new SolidColorBrush(Common.ToMediaColor(color));
                //Console.WriteLine($"Got Regular_SkillBar: {point.ToString()}, {color.ToString()}");
                dancerController.VerifyACDS();
                button_setCoordinates_skillBar.IsEnabled = true;
            });
        }
        

        private void button_setCoordinates_startDancing_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_startDancing.IsEnabled = false;
            mouseReader.GetCursorPosition((System.Drawing.Point point, int mouseButton) => {
                dancerController.ACDS.StartDancing.Coordinates = point;
                dancerController.ACDS.StartDancing.MouseAction = mouseButton;
                label_startFishing.Foreground = new SolidColorBrush(MColor.FromRgb(0, 0, 0));
                label_startFishing.Content = $"{mouseButton} {point.ToString()}";
                //Console.WriteLine($"Got StartFishing_Button: {point.ToString()}");
                dancerController.VerifyACDS();
                button_setCoordinates_startDancing.IsEnabled = true;
            });
        }


        private void button_setCoordinates_stopDancing_Click(object sender, RoutedEventArgs e)
        {
            button_setCoordinates_stopDancing.IsEnabled = false;
            mouseReader.GetCursorPosition((System.Drawing.Point point, int mouseButton) => {
                dancerController.ACDS.StopDancing.Coordinates = point;
                dancerController.ACDS.StopDancing.MouseAction = mouseButton;
                label_stopDancing.Foreground = new SolidColorBrush(MColor.FromRgb(0, 0, 0));
                label_stopDancing.Content = $"{mouseButton} {point.ToString()}";
                //Console.WriteLine($"Got StopFishing_Button: {point.ToString()}");
                dancerController.VerifyACDS();
                button_setCoordinates_startDancing.IsEnabled = true;
                groupBox_timerConfiguration.IsEnabled = true;
            });
        }

        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            dancerController.Start();
        }

        private void button_stop_Click(object sender, RoutedEventArgs e)
        {
            dancerController.Stop();
        }

        
        private void button_resetStatistics_Click(object sender, RoutedEventArgs e)
        {
            dancerController.ResetStatistics();
        }
    }
}
