using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3AssistantNativeHelper.Kits.ConnectionKit
{
    public class UILauncher : IHttpPortChangedObserver
    {
        private int port = 0;
        private ILogger log;

        public UILauncher(ILogger logger)
        {
            log = logger;
        }

        public void LaunchWebUI()
        {
            if (port < 1)
            {
                log.Warning("[UILauncher] Failed to launch UI: invalid port");
                return;
            }

            var url = $"http://localhost:{port}";

            //dashWin.webBrowser.Navigate(url);

            Process.Start(url);
        }

        public void HttpPortChanged(int port)
        {
            log.Debug("[UILauncher] Changing port to {0}", port);
            this.port = port;
        }
    }
}
