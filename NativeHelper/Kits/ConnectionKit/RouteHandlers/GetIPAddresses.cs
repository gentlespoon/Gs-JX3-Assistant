using Serilog;
using System.Net;
using Newtonsoft.Json;

namespace GsJX3AssistantNativeHelper.Kits.ConnectionKit.RouteHandlers
{
    public class RouteHandler_GetIPAddresses : Abstract_RouteHandler
    {
        public RouteHandler_GetIPAddresses(ILogger log) : base(log)
        {
        }

        public override bool Handle(HttpListenerRequest request, out string response)
        {
            response = "";
            if (request.RawUrl.StartsWith("/api/getIPAddresses"))
            {
                response = JsonConvert.SerializeObject(HttpServerKit.GetIPAddress());
                return true;
            }
            return false;
        }

    }
}
