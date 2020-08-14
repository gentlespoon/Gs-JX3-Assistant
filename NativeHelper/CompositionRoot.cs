using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using GsJX3AssistantNativeHelper.Kits.General;
using GsJX3AssistantNativeHelper.Kits.UIKit;
using GsJX3AssistantNativeHelper.Kits.ConnectionKit;
using GsJX3AssistantNativeHelper.Kits.ConnectionKit.RouteHandlers;
using GsJX3AssistantNativeHelper.Kits.HIDKits.Mouse;
using GsJX3AssistantNativeHelper.Kits.HIDKits.Display;

namespace GsJX3AssistantNativeHelper
{
    class CompositionRoot
    {
        public static IContainer SetupContainer()
        {
            var containerBuilder = new ContainerBuilder();

            // Serilog
            var logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#endif
                .WriteTo.Console()
                .WriteTo.File(AppPaths.path_logFileFullPath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            containerBuilder.Register(c => logger).As<ILogger>().SingleInstance();

            containerBuilder.RegisterType<EnvironmentSetupKit>().SingleInstance();
            containerBuilder.RegisterType<UILauncher>().SingleInstance();
            containerBuilder.RegisterType<NotifyIconKit>().SingleInstance();
            containerBuilder.RegisterType<HttpServerKit>().SingleInstance();
            containerBuilder.RegisterType<RequestHandler>().SingleInstance();

            RegisterRouteHandlers(containerBuilder);
            RegisterHIDadapters(containerBuilder);

            return containerBuilder.Build();
        }

        private static void RegisterRouteHandlers(ContainerBuilder containerBuilder)
        {
            // Route Handlers
            containerBuilder.RegisterType<RouteHandler_GetCursorCoordinates>().SingleInstance();
            containerBuilder.RegisterType<RouteHandler_GetIPAddresses>().SingleInstance();
            containerBuilder.RegisterType<RouteHandler_GetPixelColor>().SingleInstance();
            containerBuilder.RegisterType<RouteHandler_HeartBeat>().SingleInstance();
            containerBuilder.RegisterType<RouteHandler_MouseClick>().SingleInstance();
            containerBuilder.RegisterType<RouteHandler_Version>().SingleInstance();
        }

        private static void RegisterHIDadapters(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(c => new MouseSimulator_MouseEvent()).As<IMouseSimulator>().SingleInstance();
            containerBuilder.Register(c => new CursorReader_MouseKeyHook()).As<ICursorReader>().SingleInstance();
            containerBuilder.Register(c => new DisplayHelper_GDI()).As<IDisplayHelper>().SingleInstance();
        }
    }
}
