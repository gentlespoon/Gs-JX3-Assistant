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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;

using GsJX3NonInjectAssistant;
using GsJX3NonInjectAssistant.Classes.HID.Display;
using GsJX3NonInjectAssistant.Classes.HID.Mouse;
using GsJX3NonInjectAssistant.Classes.Features.Exam;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Data;

namespace GsJX3NonInjectAssistant.Views.Exam
{
    /// <summary>
    /// Interaction logic for Exam.xaml
    /// </summary>
    public partial class Exam : Page
    {

        private ExamController examController;
        private IDisplayHelper displayHelper;
        private IQAProvider qAProvider;
        private System.Timers.Timer timer_captureScreen = new System.Timers.Timer(1000);
        Bitmap CapturedScreen;

        public Exam()
        {
            InitializeComponent();
            displayHelper = new DisplayHelper_GDI();
            qAProvider = new QAProvider_LocalJSON();
            examController = new ExamController(displayHelper, qAProvider);
            timer_captureScreen.Elapsed += Timer_captureScreen_Elapsed;
            timer_captureScreen.AutoReset = true;
            //timer_captureScreen.Start();
        }

        private void Timer_captureScreen_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (examController.ScreenCaptureConfiguration.Size != Common.NullSize)
            {
                Task.Run(TakeScreenShot);
            }
        }

        public void TakeScreenShot()
        {
            Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                try
                {
                    CapturedScreen = displayHelper.CaptureScreen(examController.ScreenCaptureConfiguration);
                    image_preview.Source = Common.BitmapToImageSource(CapturedScreen);
                    TriggerOCR();
                }
                catch (Exception ex)
                {
                    Stop();
                    MessageBox.Show(ex.StackTrace, ex.Message);
                }
            }));

        }
        


        private void button_selectArea_Click(object sender, RoutedEventArgs e)
        {
            button_selectArea.IsEnabled = false;

            label_selectedArea_TL.Foreground = new SolidColorBrush(MColor.FromRgb(255, 0, 0));
            label_selectedArea_BR.Foreground = new SolidColorBrush(MColor.FromRgb(255, 0, 0));


            label_selectedArea_TL.Content = "设置文字识别框 左上角 坐标";
            label_selectedArea_BR.Content = "未设置";

            examController.ScreenCaptureConfiguration.TopLeft = Common.NullPoint;
            
            IMouseReader mouseReader_TL = new MouseReader_MouseKeyHook();

            mouseReader_TL.GetCursorPosition((System.Drawing.Point TL, int mouseButton) =>
            {
                examController.ScreenCaptureConfiguration.TopLeft = TL;
                label_selectedArea_TL.Content = $"左上{examController.ScreenCaptureConfiguration.TopLeft.ToString()}";

                label_selectedArea_BR.Content = "设置文字识别框 右下角 坐标";

                examController.ScreenCaptureConfiguration.BottomRight = Common.NullPoint;
            
                IMouseReader mouseReader_BR = new MouseReader_MouseKeyHook();
                
                mouseReader_BR.GetCursorPosition((System.Drawing.Point BR, int mouseButton) =>
                {
                    if (
                    BR.X > examController.ScreenCaptureConfiguration.TopLeft.X &&
                    BR.Y > examController.ScreenCaptureConfiguration.TopLeft.Y
                    )
                    {
                        examController.ScreenCaptureConfiguration.BottomRight = BR;
                        
                        Console.WriteLine(examController.ScreenCaptureConfiguration.Size);
                        
                        label_selectedArea_TL.Foreground = new SolidColorBrush(MColor.FromRgb(0, 0 ,0));
                        label_selectedArea_BR.Foreground = new SolidColorBrush(MColor.FromRgb(0, 0, 0));
                        label_selectedArea_BR.Content = $"右下{examController.ScreenCaptureConfiguration.BottomRight.ToString()}";
                    
                        CapturedScreen = displayHelper.CaptureScreen(examController.ScreenCaptureConfiguration);
                        image_preview.Source = Common.BitmapToImageSource(CapturedScreen);
                    
                    }
                    else
                    {
                        examController.ScreenCaptureConfiguration.TopLeft = Common.NullPoint;
                        label_selectedArea_TL.Foreground = new SolidColorBrush(MColor.FromRgb(255, 0, 0));
                        label_selectedArea_BR.Foreground = new SolidColorBrush(MColor.FromRgb(255, 0, 0));

                        label_selectedArea_TL.Content = "未设置";
                        label_selectedArea_BR.Content = "未设置";
                        MessageBox.Show("错误的文字识别区域");
                    }

                    button_selectArea.IsEnabled = true;
                    button_start.IsEnabled = true;

                });

            });

        }

        private async void Search(string question)
        {
            try
            {
                List<QuestionAndAnswer> matchedQAs = await examController.Search(question);
                listBox.ItemsSource = matchedQAs;
            }
            catch (Exception ex)
            {
                Stop();
                MessageBox.Show(ex.Message);
            }
        }


        private void TriggerOCR()
        {
            if (examController.ScreenCaptureConfiguration.Size == Common.NullSize) return;
            if (CapturedScreen == null) return;

            try
            {
                Search(examController.RunOCR(CapturedScreen));
            }
            catch (Exception ex)
            {
                Stop();
                MessageBox.Show(ex.Message);
            }

        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }



        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void button_stop_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void Start()
        {
            timer_captureScreen.Start();
            button_start.IsEnabled = false;
            button_stop.IsEnabled = true;
        }

        private void Stop()
        {
            timer_captureScreen.Stop();
            button_start.IsEnabled = true;
            button_stop.IsEnabled = false;
        }
    }
}