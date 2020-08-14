using GsJX3AssistantNativeHelper.Kits.HIDKits.Mouse;
using Serilog;
using System.Net;

namespace GsJX3AssistantNativeHelper.Kits.ConnectionKit.RouteHandlers
{
    public class RouteHandler_MouseClick : Abstract_RouteHandler
    {
        IMouseSimulator mouseSimulator;
        public RouteHandler_MouseClick(
            ILogger log,
            IMouseSimulator mouseSimulator
        ) : base(log)
        {
            this.mouseSimulator = mouseSimulator;
        }

        public override bool Handle(HttpListenerRequest request, out string response)
        {
            response = "";
            if (request.RawUrl.StartsWith("/api/mouseClick"))
            {
                log.Verbose("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                int x = int.Parse(request.QueryString.Get("X"));
                int y = int.Parse(request.QueryString.Get("Y"));
                int mb = int.Parse(request.QueryString.Get("MB"));
                bool dblClick = request.QueryString.Get("dblClick") == "1";
                mouseSimulator.Click(new System.Drawing.Point(x, y), mb, dblClick);

                return true;
            }
            return false;
        }

    }
}
