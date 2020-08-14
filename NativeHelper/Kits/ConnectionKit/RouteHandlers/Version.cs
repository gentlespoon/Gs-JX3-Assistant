using Serilog;
using System.Net;

namespace GsJX3AssistantNativeHelper.Kits.ConnectionKit.RouteHandlers
{
    public class RouteHandler_Version : Abstract_RouteHandler
    {
        public RouteHandler_Version(ILogger log) : base(log) { }

        public override bool Handle(HttpListenerRequest request, out string response)
        {
            response = "";
            if (request.RawUrl.StartsWith("/api/version"))
            {
                log.Verbose("[HTTP]<" + request.RemoteEndPoint + ">[" + request.Url + "]");
                response = (App.Current as App).nhVersion;
                return true;
            }
            return false;
        }

    }
}
