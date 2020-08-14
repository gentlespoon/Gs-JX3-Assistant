using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using GsJX3AssistantNativeHelper.Kits.HIDKits.Display;
using GsJX3AssistantNativeHelper.Kits.HIDKits.Mouse;
using System.Windows;
using System.Threading;
using System.Net.Http;
using System.Windows.Threading;
using System.Runtime.InteropServices.ComTypes;
using Serilog;
using System.Diagnostics;
using GsJX3AssistantNativeHelper.Kits.General;

namespace GsJX3AssistantNativeHelper.Kits.ConnectionKit
{
    public class HttpServerKit
    {
        private ILogger log;
        private UILauncher uiLauncher;
        private RequestHandler requestHandler;
        private EnvironmentSetupKit envSetupKit;

        private HttpListener listener;
        private Thread serverThread;

        private List<IHttpPortChangedObserver> httpPortChangedObservers = new List<IHttpPortChangedObserver>();

        private int port;

        public HttpServerKit(ILogger logger, RequestHandler requestHandler, EnvironmentSetupKit envSetupKit)
        {
            this.log = logger;
            this.requestHandler = requestHandler;
            this.envSetupKit = envSetupKit;

            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later.");
            }
        }

        public void RegisterPortChangedObserver(IHttpPortChangedObserver observer)
        {
            httpPortChangedObservers.Add(observer);
        }

        public void RegisterUILauncher(UILauncher uiLauncher)
        {
            this.uiLauncher = uiLauncher;
        }

        public static List<string> GetIPAddress()
        {
            var ipAddress = new HashSet<string>();

            string Hostname = Environment.MachineName;
            IPHostEntry Host = Dns.GetHostEntry(Hostname);
            
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddress.Add(IP.ToString());
                }
            }
            return ipAddress.ToList();
        }

        private void Listen()
        {
            try
            {
                while (listener.IsListening)
                {
                    var requestContext = listener.GetContext();
                    if (requestContext != null)
                    {
                        Task.Run(() =>
                        {
                            log.Verbose("[HTTP]<" + requestContext.Request.RemoteEndPoint + ">[" + requestContext.Request.Url + "]");
                            requestHandler.HandleHttpListenerContext(requestContext);
                        });
                    }
                }
            }
            catch
            {
                // mute exception
            }
        }

        public void Start()
        {
            port = (new Random()).Next(5000, 65500);

            if (listener != null)
            {
                if (listener.IsListening)
                {
                    Stop();
                }
            }

            envSetupKit.SetupFirewallPortRule((ushort)port);
            
            listener = new HttpListener();
            listener.Prefixes.Add($"http://*:{port}/");
            listener.Start();

            log.Information("[HttpServerKit] HTTP server started, listening on port {0}", port);

            foreach (IHttpPortChangedObserver httpPortChangedObserver in httpPortChangedObservers)
            {
                httpPortChangedObserver.HttpPortChanged(port);
            }

            serverThread = new Thread(this.Listen);
            serverThread.Start();
        }

        public void Stop()
        {
            log.Warning("[HttpServerKit] Listener stopped.");
            listener.Stop();
            listener.Close();
        }

        


    }
}