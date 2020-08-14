using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3AssistantNativeHelper
{
    public static class AppPaths
    {
        // %APPDATA%
        public static string path_AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string path_AppPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static string path_AppDirectory = Path.GetDirectoryName(path_AppPath);
        public static string path_AppUIDirectory = Path.Combine(path_AppDirectory, "UI");

        // Setup log file path
        //   Filename will be set at launch time.
        //   Main purpose is to separate files to prevent huge log files,
        //     rather than using accurate time. 
        public static string path_logFileName = @"log-.log";
        public static string path_logFileFullPath = Path.Combine(path_AppDirectory, "log", path_logFileName);

        public static string path_configFileName = "config.json";
        public static string path_configFileFullPath = Path.Combine(path_AppDirectory, path_configFileName);
    }
}
