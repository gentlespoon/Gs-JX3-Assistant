using Serilog;
using System.Net;

namespace GsJX3AssistantNativeHelper.Kits.ConnectionKit.RouteHandlers
{
    public class RouteHandler_HeartBeat : Abstract_RouteHandler
    {
        public RouteHandler_HeartBeat(ILogger log) : base(log) { }

        public override bool Handle(HttpListenerRequest request, out string response)
        {
            response = "";
            if (request.RawUrl.StartsWith("/api/heartBeat"))
            {
                log.Verbose("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                return true;
            }
            return false;
        }

    }
}
