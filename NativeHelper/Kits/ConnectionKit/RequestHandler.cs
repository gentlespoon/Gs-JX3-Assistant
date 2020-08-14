using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using GsJX3AssistantNativeHelper.Kits.ConnectionKit.RouteHandlers;
using Serilog;

namespace GsJX3AssistantNativeHelper.Kits.ConnectionKit
{
    public class RequestHandler
    {
        private ILogger log;
        private List<Abstract_RouteHandler> routeHandlers = new List<Abstract_RouteHandler>();
        public RequestHandler(
            ILogger logger,

            RouteHandler_GetCursorCoordinates rh_getCursorCoordinates,
            RouteHandler_GetIPAddresses rh_getIPAddresses,
            RouteHandler_GetPixelColor rh_getPixelColor,
            RouteHandler_HeartBeat rh_heartBeat,
            RouteHandler_MouseClick rh_mouseClick,
            RouteHandler_Version rh_version
        )
        {
            this.log = logger;

            // Initialize request handlers
            routeHandlers.Add(rh_getCursorCoordinates);
            routeHandlers.Add(rh_getIPAddresses);
            routeHandlers.Add(rh_getPixelColor);
            routeHandlers.Add(rh_heartBeat);
            routeHandlers.Add(rh_mouseClick);
            routeHandlers.Add(rh_version);

        }

        public void HandleHttpListenerContext(HttpListenerContext ctx)
        {
            try
            {
                ctx.Response.Headers.Add("Server", "");
                // ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                if (ctx.Request.RawUrl.StartsWith("/api"))
                {
                    RunAPIHandlers(ctx);
                }
                else
                {
                    RunStaticFileHandlers(ctx);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            finally
            {
                // always close the stream
                if (ctx != null)
                {
                    ctx.Response.OutputStream.Flush();
                    ctx.Response.Close();
                }
            }
        }

        private bool RunAPIHandlers(HttpListenerContext ctx)
        {
            string response = "";
            // Loop through handlers
            foreach (Abstract_RouteHandler routerHandler in routeHandlers)
            {
                if (routerHandler.Handle(ctx.Request, out response))
                {
                    var buf = Encoding.UTF8.GetBytes(response);
                    ctx.Response.ContentLength64 = buf.Length;
                    ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                    ctx.Response.StatusCode = (int)HttpStatusCode.OK;
                    return true;
                }
            }
            // return 400 if no handler found
            ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return false;
        }

        private bool RunStaticFileHandlers(HttpListenerContext ctx)
        {

            string fileName = ctx.Request.Url.AbsolutePath;
            
            fileName = fileName.Substring(1);

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "index.html";
            }
            string filePath = Path.Combine(AppPaths.path_AppUIDirectory, fileName);
            log.Debug("[RequestHandler] Serving {0}", filePath);
            if (!File.Exists(filePath)) {
                filePath = Path.Combine(AppPaths.path_AppUIDirectory, "index.html");
            }

            try
            {
                Stream input = new FileStream(filePath, FileMode.Open);

                //Adding permanent http response headers
                string mime;
                ctx.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filePath), out mime)
                    ? mime
                    : "application/octet-stream";
                ctx.Response.ContentLength64 = input.Length;
                //ctx.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                //ctx.Response.AddHeader("Last-Modified", File.GetLastWriteTime(filePath).ToString("r"));

                byte[] buffer = new byte[1024 * 32];
                int nbytes;
                while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    ctx.Response.OutputStream.Write(buffer, 0, nbytes);
                input.Close();
                ctx.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {

                var buf = Encoding.UTF8.GetBytes("Failed to load index.html");
                ctx.Response.ContentLength64 = buf.Length;
                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return true;
        }

        /// <summary>
        /// Mime Type conversion table
        /// </summary>
        private static IDictionary<string, string> _mimeTypeMappings =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                #region extension to MIME type list
                {".asf", "video/x-ms-asf"},
                {".asx", "video/x-ms-asf"},
                {".avi", "video/x-msvideo"},
                {".bin", "application/octet-stream"},
                {".cco", "application/x-cocoa"},
                {".crt", "application/x-x509-ca-cert"},
                {".css", "text/css"},
                {".deb", "application/octet-stream"},
                {".der", "application/x-x509-ca-cert"},
                {".dll", "application/octet-stream"},
                {".dmg", "application/octet-stream"},
                {".ear", "application/java-archive"},
                {".eot", "application/octet-stream"},
                {".exe", "application/octet-stream"},
                {".flv", "video/x-flv"},
                {".gif", "image/gif"},
                {".hqx", "application/mac-binhex40"},
                {".htc", "text/x-component"},
                {".htm", "text/html"},
                {".html", "text/html"},
                {".ico", "image/x-icon"},
                {".img", "application/octet-stream"},
                {".iso", "application/octet-stream"},
                {".jar", "application/java-archive"},
                {".jardiff", "application/x-java-archive-diff"},
                {".jng", "image/x-jng"},
                {".jnlp", "application/x-java-jnlp-file"},
                {".jpeg", "image/jpeg"},
                {".jpg", "image/jpeg"},
                {".js", "application/x-javascript"},
                {".mml", "text/mathml"},
                {".mng", "video/x-mng"},
                {".mov", "video/quicktime"},
                {".mp3", "audio/mpeg"},
                {".mpeg", "video/mpeg"},
                {".mpg", "video/mpeg"},
                {".msi", "application/octet-stream"},
                {".msm", "application/octet-stream"},
                {".msp", "application/octet-stream"},
                {".pdb", "application/x-pilot"},
                {".pdf", "application/pdf"},
                {".pem", "application/x-x509-ca-cert"},
                {".pl", "application/x-perl"},
                {".pm", "application/x-perl"},
                {".png", "image/png"},
                {".prc", "application/x-pilot"},
                {".ra", "audio/x-realaudio"},
                {".rar", "application/x-rar-compressed"},
                {".rpm", "application/x-redhat-package-manager"},
                {".rss", "text/xml"},
                {".run", "application/x-makeself"},
                {".sea", "application/x-sea"},
                {".shtml", "text/html"},
                {".sit", "application/x-stuffit"},
                {".swf", "application/x-shockwave-flash"},
                {".tcl", "application/x-tcl"},
                {".tk", "application/x-tcl"},
                {".txt", "text/plain"},
                {".war", "application/java-archive"},
                {".wbmp", "image/vnd.wap.wbmp"},
                {".wmv", "video/x-ms-wmv"},
                {".xml", "text/xml"},
                {".xpi", "application/x-xpinstall"},
                {".zip", "application/zip"},

                #endregion
            };
    }
}
