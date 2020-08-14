using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3AssistantNativeHelper.Kits.ConnectionKit
{
    public abstract class Abstract_RouteHandler
    {
        protected ILogger log;
        public Abstract_RouteHandler(ILogger log)
        {
            this.log = log;
        }

        /// <summary>
        /// Handles a specific route
        /// </summary>
        /// <param name="request"></param>
        /// <returns>True if route is handled. False if not handled.</returns>
        public abstract bool Handle(HttpListenerRequest request, out string response);
    }
}
