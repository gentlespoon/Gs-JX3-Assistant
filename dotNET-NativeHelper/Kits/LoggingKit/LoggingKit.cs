using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GsJX3AssistantNativeHelper.Kits
{
    public class LoggingKit
    {
        public MainWindow mainWindow;
        
        private List<string> _logs = new List<string>();
        private uint _maxLogCount = 200;
        
        public delegate void NotifyLogUpdated();
        public List<NotifyLogUpdated> logUpdatedListeners = new List<NotifyLogUpdated>();

        private StreamWriter _logFileWriter = null;

        private bool _verboseLogging = ((App)Application.Current).verboseLogging;

        public LoggingKit(string logFilePath)
        {
            _logFileWriter = File.AppendText(logFilePath);
            _logFileWriter.WriteLine("\n====================");
        }

        public bool addNotifyLogListener(NotifyLogUpdated notifyLogUpdated)
        {
            this.logUpdatedListeners.Add(notifyLogUpdated);
            return true;
        }


        private string logFormatter(string message)
        {
            string time = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
            message = "[" + time + "]" + message;
            return message;
        }

        private void addLogEntry(string message)
        {
            if (_logs.Count >= _maxLogCount)
            {
                _logs.RemoveAt(0);
            }
            message = logFormatter(message);

            Console.WriteLine(message);
            _logs.Add(message);
            _logFileWriter.WriteLine(message);
            _logFileWriter.Flush();
            
            foreach(NotifyLogUpdated notify in logUpdatedListeners)
            {
                notify();
            }
        }

        public void info(string message)
        {
            message = "[INFO] " + message;
            addLogEntry(message);
        }

        public void error(string message)
        {
            message = "[ERRO] " + message;
            addLogEntry(message);
        }

        public void warn(string message)
        {
            message = "[WARN] " + message;
            addLogEntry(message);
        }

        public void debug(string message)
        {
            message = "[DEBU] " + message;
            addLogEntry(message);
        }

        public void verbose(string message)
        {
            if (!_verboseLogging) return;
            message = "[VERB] " + message;
            addLogEntry(message);
        }

        public string logs
        {
            get
            {
                string log = "";
                foreach(string logEntry in _logs)
                {
                    log += logEntry + '\n';
                }
                return log;
            }
        }
    }
}
