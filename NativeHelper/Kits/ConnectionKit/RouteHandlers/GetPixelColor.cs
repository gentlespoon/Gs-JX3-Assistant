using GsJX3AssistantNativeHelper.Kits.HIDKits.Display;
using Serilog;
using System.Net;

namespace GsJX3AssistantNativeHelper.Kits.ConnectionKit.RouteHandlers
{
    public class RouteHandler_GetPixelColor : Abstract_RouteHandler
    {
        private IDisplayHelper displayHelper;

        public RouteHandler_GetPixelColor(
            ILogger log,
            IDisplayHelper displayHelper
        ) : base(log) {
            this.displayHelper = displayHelper;
        }

        public override bool Handle(HttpListenerRequest request, out string response)
        {
            response = "";
            if (request.RawUrl.StartsWith("/api/getPixelColor"))
            {
                log.Verbose("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                int x = int.Parse(request.QueryString.Get("X"));
                int y = int.Parse(request.QueryString.Get("Y"));
                System.Drawing.Color pixelColor = displayHelper.GetColorAt(new System.Drawing.Point(x, y));
                response = "{\"R\":" + pixelColor.R.ToString() + ",\"G\":" + pixelColor.G.ToString() + ",\"B\":" + pixelColor.B.ToString() + "}";
                return true;
            }
            return false;
        }

    }
}
