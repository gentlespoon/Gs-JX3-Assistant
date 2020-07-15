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
        public static string path_AppData = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string path_AppPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static string path_AppDirectory = System.IO.Path.GetDirectoryName(path_AppPath);
        // Setup log file path
        //   Filename will be set at launch time.
        //   Main purpose is to separate files to prevent huge log files,
        //     rather than using accurate time. 
        public static string path_logFileName = DateTime.Now.ToString("yyyyMMdd") + @".log";
        public static string path_logFileFullPath = Path.Combine(path_AppDirectory, "log", path_logFileName);
    }
}
