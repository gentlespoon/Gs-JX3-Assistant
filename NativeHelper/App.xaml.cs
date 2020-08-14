using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using System.Web;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.IO;
using System.Drawing;
using Autofac;
using Serilog;
using GsJX3AssistantNativeHelper.Kits.UIKit;
using GsJX3AssistantNativeHelper.Kits.ConnectionKit;
using GsJX3AssistantNativeHelper.Kits.General;

namespace GsJX3AssistantNativeHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal string nhVersion = "20.08.13.1900";

        private IContainer container;

        private ILogger log;
        private HttpServerKit httpServerKit;
        private NotifyIconKit notifyIconKit;
        private EnvironmentSetupKit envSetupKit;

        public App()
        {
            container = CompositionRoot.SetupContainer();

            log = container.Resolve<ILogger>();
            log.Information("==== NEW SESSION ====");
            log.Information($"Application starting up (version {nhVersion})");
            log.Information($"Startup location {AppPaths.path_AppPath}");
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            notifyIconKit = container.Resolve<NotifyIconKit>();
            httpServerKit = container.Resolve<HttpServerKit>();

            log.Debug("[Application_Startup] Showing NotifyIcon");
            notifyIconKit.Show();

            var uiLauncher = container.Resolve<UILauncher>();
            httpServerKit.RegisterUILauncher(uiLauncher);
            notifyIconKit.RegisterUILauncher(uiLauncher);
            httpServerKit.RegisterPortChangedObserver(notifyIconKit);
            httpServerKit.RegisterPortChangedObserver(uiLauncher);

            // Start server
            log.Debug("[Application_Startup] Launching HTTP Server");
            httpServerKit.Start();

            log.Debug("[Application_Startup] Launching Web UI");
            uiLauncher.LaunchWebUI();
        }

        public void Terminate()
        {
            log?.Debug("[Terminate] Starting Terminate sequence");
            envSetupKit?.RemoveFirewallPortRule();
            log?.Debug("[Terminate] Removed Firewall port rule");
            httpServerKit?.Stop();
            log?.Debug("[Terminate] Stopped HTTP Server");
            notifyIconKit?.Hide();
            log?.Debug("[Terminate] Hide NotifyIcon");
            Thread.Sleep(1000);
            log?.Debug("[Terminate] Env Exit");
            Environment.Exit(Environment.ExitCode);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Terminate();
        }
    }
}
