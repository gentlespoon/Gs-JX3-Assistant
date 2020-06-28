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
        private HttpListener _listener;
        private LoggingKit _loggingKit;

        public HttpServerKit(LoggingKit loggingKit)
        {
            _loggingKit = loggingKit;

            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later.");
            }
        }


        public void start(int port)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:" + port + "/");
            _listener.Start();

            // listen on a background thread
            Task.Run(() =>
            {
                _loggingKit.info("Application started, listening on port " + port);

                try
                {
                    while (_listener.IsListening)
                    {
                        var requestContext = _listener.GetContext();
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
                                            string response = handleHttpRequest(ctx.Request);
                                            var buf = Encoding.UTF8.GetBytes(response);
                                            ctx.Response.ContentLength64 = buf.Length;
                                            ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _loggingKit.error(ex.ToString());
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
                    _loggingKit.error(ex.ToString());
                }

            });
        }

        public void stop()
        {
            _listener.Stop();
            _listener.Close();
        }

        public void heartBeat()
        {
            // heartbeat request
            Application.Current.Dispatcher.Invoke(() =>
            {
                _loggingKit.verbose("Heartbeat detected. Resetting suicide countdown");
                ((Application.Current as App).MainWindow as MainWindow).resetSuicideCounter();
            });
        }


        public string handleHttpRequest(HttpListenerRequest request)
        {
            _loggingKit.info("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
            string response = "";

            if (request.RawUrl.StartsWith("/heartBeat"))
            {
                heartBeat();
            } else
            
            
            if (request.RawUrl.StartsWith("/version")) {
                response = (Application.Current as App).nhVersion;
            } else
            
            
            if (request.RawUrl.StartsWith("/visible"))
            {
                bool isVisible = request.QueryString.Get("visible") == "true";
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (isVisible)
                    {
                        (Application.Current.MainWindow).Show();
                    }
                    else
                    {
                        (Application.Current.MainWindow).Hide();
                    }
                });
            } else
            
            
            if (request.RawUrl.StartsWith("/getPixelColor"))
            {
                int x = int.Parse(request.QueryString.Get("X"));
                int y = int.Parse(request.QueryString.Get("Y"));
                IDisplayHelper displayHelper = new DisplayHelper_GDI();
                System.Drawing.Color pixelColor = displayHelper.GetColorAt(new System.Drawing.Point(x, y));
                response = "{\"R\":"+pixelColor.R.ToString() + ",\"G\":" + pixelColor.G.ToString() + ",\"B\":" + pixelColor.B.ToString()+"}";
                _loggingKit.info("Cursor Coordinates: " + response);
            } else
            

            if (request.RawUrl.StartsWith("/getCursorCoordinates"))
            {
                AutoResetEvent stopWaitHandle = new AutoResetEvent(false);
                // Must be done on UI thread
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ICursorReader cursorReader = new CursorReader_MouseKeyHook();
                    cursorReader.GetCursorPosition((System.Drawing.Point point, int mouseButton) =>
                    {
                        response = "{\"X\":" + point.X + ",\"Y\":" + point.Y + ",\"MB\":" + mouseButton + "}";
                        _loggingKit.info(response);
                        stopWaitHandle.Set();
                    });
                });
                stopWaitHandle.WaitOne();
            }
            else


            if (request.RawUrl.StartsWith("/mouseClickAt"))
            {
                int x = int.Parse(request.QueryString.Get("X"));
                int y = int.Parse(request.QueryString.Get("Y"));
                int mb = int.Parse(request.QueryString.Get("MB"));

                IMouseSimulator mouseSimulator = new MouseSimulator_MouseEvent();
                mouseSimulator.Click(new System.Drawing.Point(x, y), mb);
            }

            return response;
        }


    }
}