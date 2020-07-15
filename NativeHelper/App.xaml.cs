using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using GsJX3AssistantNativeHelper.Kits;
using System.Web;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.IO;
using System.Drawing;

namespace GsJX3AssistantNativeHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        internal string nhVersion = "20.06.28.1903";

        
        private LoggingKit loggingKit;
        private HttpServerKit httpServerKit;
        private TimeoutKit timeoutKit;
        private NotifyIconKit notifyIconKit;

        public App()
        {
            Console.WriteLine(AppPaths.path_logFileFullPath);

            // Setup logging
            loggingKit = new LoggingKit(AppPaths.path_logFileFullPath, false);
            loggingKit.Info($"Application starting up (version {nhVersion}) at {AppPaths.path_AppPath}");

            notifyIconKit = new NotifyIconKit();
            notifyIconKit.Normal();

            // register scheme handler
            var schemeRegisterKit = new SchemeRegisterKit(loggingKit);
            schemeRegisterKit.registerSchemeHandler();

            timeoutKit = new TimeoutKit(10, Terminate, notifyIconKit, loggingKit);
            timeoutKit.Start();

            
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            int httpPort = (new Random()).Next(5000, 65500);
            bool openBrowser = true;
            if (e.Args.Length == 1)
            {
                // if started by URL Scheme
                string arg = e.Args[0];
                arg = arg.Substring(SchemeRegisterKit.protocol.Length + 3);
                if (arg.EndsWith("/"))
                {
                    Console.WriteLine("Removing trailing /");
                    arg = arg.Remove(arg.Length - 1);
                }

                var parsedQS = HttpUtility.ParseQueryString(arg);

                foreach (string key in parsedQS)
                {
                    loggingKit.Info("[StartupArgs] " + key + ": " + parsedQS[key]);
                    switch (key)
                    {
                        case "port":
                            try
                            {
                                httpPort = int.Parse(parsedQS[key]);
                                openBrowser = false;
                            }
                            catch
                            {
                                var errorMsg = $"Failed to parse HTTP port from {parsedQS[key]}";
                                loggingKit.Error(errorMsg);
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
            else
            {
                loggingKit.Warn($"Started without arguments.");
            }

            httpServerKit = new HttpServerKit(httpPort, Terminate, timeoutKit, loggingKit);
            httpServerKit.Start();

            if (openBrowser)
            {
                Process.Start($"https://jx3.gentlespoon.com/automator/connect?port={httpPort}");
            }
        }

        

        public void Terminate()
        {
            httpServerKit?.Stop();
            timeoutKit?.Stop();
            notifyIconKit?.Hide();
            Thread.Sleep(1000);
            Environment.Exit(Environment.ExitCode);
        }



    }
}
