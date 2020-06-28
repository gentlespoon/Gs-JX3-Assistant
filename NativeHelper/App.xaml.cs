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

namespace GsJX3AssistantNativeHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public bool verboseLogging = false;
        public string logFilePath = DateTime.Now.ToString("yyyyMMdd") + ".log";
        public int httpPort = 8080;
        public bool hideMainWindow = false;

        public string nhVersion = "20.06.28.0042";


        public LoggingKit loggingKit;
        public SchemeRegisterKit schemeRegisterKit;

        public App()
        {
            loggingKit = new LoggingKit(logFilePath);
            schemeRegisterKit = new SchemeRegisterKit(loggingKit);
            
            schemeRegisterKit.registerSchemeHandler();

            
            

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
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

                foreach(string key in parsedQS)
                {
                    loggingKit.info("[StartUpArgs] " + key + ": " + parsedQS[key]);
                    switch (key)
                    {
                        case "port":
                            //httpPort = int.Parse(parsedQS[key]);
                            int.TryParse(parsedQS[key], out httpPort);
                            break;
                        case "window":
                            if (parsedQS[key] == "hide")
                            {
                                hideMainWindow = true;
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
            else
            {
                
            }
        }

        public void Terminate()
        {
            Environment.Exit(Environment.ExitCode);
        }

        

    }
}
