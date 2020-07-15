using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using GsJX3AssistantNativeHelper.Kits;
using System.Windows;
using System.Threading;
using System.Net.Http;
using System.Windows.Threading;
using System.Runtime.InteropServices.ComTypes;

namespace GsJX3AssistantNativeHelper.Kits
{

    public class HttpServerKit
    {
        private HttpListener listener;
        private LoggingKit loggingKit;
        private TimeoutKit timeoutKit;

        public delegate void TerminateDelegate();
        private TerminateDelegate terminate;

        private int port;

        public HttpServerKit(int port, TerminateDelegate terminate, TimeoutKit timeoutKit, LoggingKit loggingKit)
        {
            this.loggingKit = loggingKit;
            this.timeoutKit = timeoutKit;
            this.port = port;
            this.terminate = terminate;

            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later.");
            }
        }


        public void Start()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:" + port + "/");
            listener.Start();

            // listen on a background thread
            Task.Run(() =>
            {
                loggingKit.Info("HTTP server started, listening on port " + port);

                try
                {
                    while (listener.IsListening)
                    {
                        var requestContext = listener.GetContext();
                        if (requestContext != null)
                        {
                            Task.Run(() =>
                            {
                                var ctx = requestContext;
                                try
                                {
                                    ctx.Response.Headers.Add("Server", "");
                                    ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                                    switch (ctx.Request.HttpMethod.ToUpper())
                                    {
                                        case "OPTIONS":
                                            // CORS request
                                            break;
                                        default:
                                            string response = HandleHttpRequest(ctx.Request);
                                            var buf = Encoding.UTF8.GetBytes(response);
                                            ctx.Response.ContentLength64 = buf.Length;
                                            ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    loggingKit.Error(ex.ToString());
                                }
                                finally
                                {
                                    // always close the stream
                                    if (ctx != null)
                                    {
                                        ctx.Response.OutputStream.Close();
                                    }
                                }
                            });
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    // mute exception
                }

            });
        }

        public void Stop()
        {
            loggingKit.Warn("Listener stopped.");
            listener.Stop();
            listener.Close();
        }

        public string HandleHttpRequest(HttpListenerRequest request)
        {
            string response = "";

            if (request.RawUrl.StartsWith("/heartBeat"))
            {
                loggingKit.Verbose("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                // heartbeat request
                Application.Current.Dispatcher.Invoke(() =>
                {
                    loggingKit.Verbose("Heartbeat detected. Resetting suicide countdown");
                    timeoutKit.ResetTimeout();
                });
            } else


            if (request.RawUrl.StartsWith("/shutdown"))
            {
                loggingKit.Warn("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                Application.Current.Dispatcher.Invoke(() =>
                {
                    response = "1";
                    terminate();
                });
            }
            else


            if (request.RawUrl.StartsWith("/version"))
            {
                loggingKit.Info("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                response = (Application.Current as App).nhVersion;
            } else
            
            
            if (request.RawUrl.StartsWith("/getPixelColor"))
            {
                loggingKit.Info("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                int x = int.Parse(request.QueryString.Get("X"));
                int y = int.Parse(request.QueryString.Get("Y"));
                IDisplayHelper displayHelper = new DisplayHelper_GDI();
                System.Drawing.Color pixelColor = displayHelper.GetColorAt(new System.Drawing.Point(x, y));
                response = "{\"R\":"+pixelColor.R.ToString() + ",\"G\":" + pixelColor.G.ToString() + ",\"B\":" + pixelColor.B.ToString()+"}";
                loggingKit.Info(response);
            } else
            

            if (request.RawUrl.StartsWith("/getCursorCoordinates"))
            {
                loggingKit.Info("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                AutoResetEvent stopWaitHandle = new AutoResetEvent(false);
                // Must be done on UI thread or Garbage Collection will freeze the cursor
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ICursorReader cursorReader = new CursorReader_MouseKeyHook();
                    cursorReader.GetCursorPosition((System.Drawing.Point point, int mouseButton) =>
                    {
                        response = "{\"X\":" + point.X + ",\"Y\":" + point.Y + ",\"MB\":" + mouseButton + "}";
                        loggingKit.Info(response);
                        stopWaitHandle.Set();
                    });
                });
                stopWaitHandle.WaitOne();
            }
            else


            if (request.RawUrl.StartsWith("/mouseClickAt"))
            {
                loggingKit.Info("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                int x = int.Parse(request.QueryString.Get("X"));
                int y = int.Parse(request.QueryString.Get("Y"));
                int mb = int.Parse(request.QueryString.Get("MB"));
                bool dblClick = request.QueryString.Get("dblClick") == "1";

                IMouseSimulator mouseSimulator = new MouseSimulator_MouseEvent();
                mouseSimulator.Click(new System.Drawing.Point(x, y), mb, dblClick);
            }

            return response;
        }


    }
}