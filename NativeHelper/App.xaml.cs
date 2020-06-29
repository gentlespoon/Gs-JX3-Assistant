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

namespace GsJX3AssistantNativeHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public bool verboseLogging = false;
        public string logFilePath = DateTime.Now.ToString("yyyyMMdd") + ".log";
        public int httpPort = 65512;
        public bool visible = true;

        public string nhVersion = "20.06.28.1802";


        public LoggingKit loggingKit;
        public SchemeRegisterKit schemeRegisterKit;

        public App()
        {
            
            // Extract embedded DLL
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            loggingKit = new LoggingKit(logFilePath);
            schemeRegisterKit = new SchemeRegisterKit(loggingKit);
            
            schemeRegisterKit.registerSchemeHandler();
        }

        // Extract embedded DLL
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string resourceName = "GsJX3AssistantNativeHelper." + new AssemblyName(args.Name).Name + ".dll";
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                byte[] assemblyData = new byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }

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
                    loggingKit.Info("[StartUpArgs] " + key + ": " + parsedQS[key]);
                    switch (key)
                    {
                        case "port":
                            //httpPort = int.Parse(parsedQS[key]);
                            int.TryParse(parsedQS[key], out httpPort);
                            break;
                        case "visible":
                            if (parsedQS[key] == "false")
                            {
                                visible = false;
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
            else
            {
                loggingKit.Error("Started without arguments. Opening webpage.");
                Process.Start("https://jx3.gentlespoon.com/automator");
                Terminate();

            }
        }

        public void Terminate()
        {
            Environment.Exit(Environment.ExitCode);
        }

        

    }
}
