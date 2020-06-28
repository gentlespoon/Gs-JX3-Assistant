using GsJX3AssistantNativeHelper.Kits;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace GsJX3AssistantNativeHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private LoggingKit _loggingKit;
        private int suicideTimeout = 15;
        public int suicideCounter;
        public System.Timers.Timer suicideTimer;

        private delegate void ShutDownDelegate();
        private ShutDownDelegate shutdownDelegate;


        public HttpServerKit httpServerKit;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;

            if (app.hideMainWindow)
            {
                this.Hide();
            }
            shutdownDelegate = app.Terminate;
            _loggingKit = app.loggingKit;


            httpServerKit = new HttpServerKit(_loggingKit);
            httpServerKit.start(app.httpPort);

            // terminate self if not getting heartbeat
            resetSuicideCounter();
            suicideTimer = new System.Timers.Timer(1000);
            suicideTimer.Elapsed += (s, e) =>
            {
                suicideCounter--;
                _loggingKit.verbose("SuicideCounter = " + suicideCounter);
                NotifyPropertyChanged("suicideCountdownString");
                if (suicideCounter <= 0)
                {
                    suicide("Heartbeat timeout");
                }
            };
            suicideTimer.Enabled = true;

        }

        public string suicideCountdownString
        {
            get
            {
                return suicideCounter.ToString();
            }
        }

        public string logFilePath
        {
            get
            {
                return (Application.Current as App).logFilePath;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void suicide(string reason)
        {
            suicideTimer.Enabled = false;
            _loggingKit.warn("Shutting down: " + reason);
            httpServerKit.stop();
            shutdownDelegate();
        }

        public void resetSuicideCounter()
        {
            suicideCounter = suicideTimeout;
        }

        private void logfilePath_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(System.IO.Path.GetFullPath(logFilePath));
        }
    }

}
