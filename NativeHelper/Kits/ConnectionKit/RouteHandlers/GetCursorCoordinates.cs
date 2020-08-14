using GsJX3AssistantNativeHelper.Kits.HIDKits.Mouse;
using Serilog;
using System.Net;
using System.Threading;

namespace GsJX3AssistantNativeHelper.Kits.ConnectionKit.RouteHandlers
{
    public class RouteHandler_GetCursorCoordinates : Abstract_RouteHandler
    {
        ICursorReader cursorReader;

        public RouteHandler_GetCursorCoordinates(
            ILogger log,
            ICursorReader cursorReader
        ) : base(log) {
            this.cursorReader = cursorReader;
        }

        public override bool Handle(HttpListenerRequest request, out string response)
        {
            response = "";
            if (request.RawUrl.StartsWith("/api/getCursorCoordinates"))
            {
                log.Verbose("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                AutoResetEvent stopWaitHandle = new AutoResetEvent(false);

                string r = "";
                //Must be done on UI thread or Garbage Collection will freeze the cursor
                App.Current.Dispatcher.Invoke(() =>
                {
                    cursorReader.GetCursorPosition((System.Drawing.Point point, int mouseButton) =>
                    {
                        r = "{\"X\":" + point.X + ",\"Y\":" + point.Y + ",\"MB\":" + mouseButton + "}";
                        log.Debug("[HttpServerKit] " + r);
                        stopWaitHandle.Set();
                    });
                });

                stopWaitHandle.WaitOne();
                response = r;
                return true;
            }
            return false;
        }

    }
}
