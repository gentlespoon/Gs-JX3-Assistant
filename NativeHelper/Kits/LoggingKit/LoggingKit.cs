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
        
        public delegate void NotifyLogUpdated();
        public List<NotifyLogUpdated> logUpdatedListeners = new List<NotifyLogUpdated>();

        public Queue<string> unprintedLogs = new Queue<string>();

        private StreamWriter _logFileWriter = null;

        private bool _verboseLogging = ((App)Application.Current).verboseLogging;

        public LoggingKit(string logFilePath)
        {
            string fullPath = Path.GetFullPath(logFilePath);
            _logFileWriter = File.AppendText(fullPath);
            _logFileWriter.WriteLine("\n====================");
            _logFileWriter.WriteLine(fullPath);
        }

        private string LogFormatter(string message)
        {
            string time = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
            message = "[" + time + "]" + message;
            return message;
        }

        private void AddLogEntry(string message)
        {
            message = LogFormatter(message);

            Console.WriteLine(message);
            _logFileWriter.WriteLine(message);
            _logFileWriter.Flush();

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Application.Current.MainWindow == null || (Application.Current.MainWindow as MainWindow).textBox_logs == null)
                {
                    unprintedLogs.Enqueue(message);
                }
                else
                {
                    while (unprintedLogs.Count > 0)
                    {
                        string logEntry = unprintedLogs.Dequeue();
                        (Application.Current.MainWindow as MainWindow).textBox_logs.AppendText(logEntry + "\n");
                    }
                    (Application.Current.MainWindow as MainWindow).textBox_logs.AppendText(message + "\n");
                    (Application.Current.MainWindow as  MainWindow).textBox_logs.ScrollToEnd();
                }
            });
        }

        public void Info(string message)
        {
            message = "[INFO] " + message;
            AddLogEntry(message);
        }

        public void Error(string message)
        {
            message = "[ERRO] " + message;
            AddLogEntry(message);
        }

        public void Warn(string message)
        {
            message = "[WARN] " + message;
            AddLogEntry(message);
        }

        public void Debug(string message)
        {
            message = "[DEBU] " + message;
            AddLogEntry(message);
        }

        public void Verbose(string message)
        {
            if (!_verboseLogging) return;
            message = "[VERB] " + message;
            AddLogEntry(message);
        }

    }
}
