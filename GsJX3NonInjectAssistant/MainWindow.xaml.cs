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
using Forms = System.Windows.Forms;

namespace GsJX3NonInjectAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static Label static_label_versionStatus = null;

        //private OverlayWindow overlayWindow = new OverlayWindow();

        public async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //setTopMost(true);
            //Top = 0;
            static_label_versionStatus = label_versionStatus;
            label_version.Content = Constants.Version;
            if (await CheckUpdate.Check())
            {
                GetUpdate();
            }

            //overlayWindow.Show();
        }

        private void checkbox_topMost_Click(object sender, RoutedEventArgs e)
        {
        }

        //private void setTopMost(bool topMost)
        //{
        //    if (topMost)
        //    {
        //        this.Topmost = true;
        //        this.label_topMost.Content = "√ 保持窗口可见";
        //    } else
        //    {
        //        this.Topmost = false;
        //        this.label_topMost.Content = "x 保持窗口可见";
        //    }
        //}

        public static void SetVersionStatus(string msg, Brush brush)
        {
            if (null != static_label_versionStatus)
            {
                static_label_versionStatus.Content = msg;
                static_label_versionStatus.Foreground = brush;
            }
        }

        //private void label_topMost_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    setTopMost(!Topmost);
        //}

        private void label_versionStatus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GetUpdate();
        }

        private void GetUpdate()
        {
            if (CheckUpdate.newerVersions.Count > 0)
            {
                var newestVersion = CheckUpdate.newerVersions[CheckUpdate.newerVersions.Count - 1];
                var response = MessageBox.Show(
                    $"{newestVersion.Key}\n" +
                    $"{newestVersion.Value}\n\n" +
                    $"下载新版本？", "更新", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (response == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(CheckUpdate.ReleaseUrl);
                }
            }
        }

        // Borderless Window Movements
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //overlayWindow.Close();
            Close();
        }

        // Auto Hide
        bool EnableAutoHide = false;
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Top <= 0)
            {
                EnableAutoHide = true;
            }
            else
            {
                EnableAutoHide = false;
            }

            if (EnableAutoHide)
            {
                Height = 3;
                //Top = -Height + 2;
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            if (EnableAutoHide)
            {
                Height = 502;
                //Top = 0;
            }
        }

    }
}
