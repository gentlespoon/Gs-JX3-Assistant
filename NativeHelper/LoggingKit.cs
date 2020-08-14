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
        private StreamWriter _logFileWriter = null;
        private bool _verboseLogging;

        public LoggingKit(string logFilePath, bool isVerbose = false)
        {
            string logDirectory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logDirectory)) {
                Directory.CreateDirectory(logDirectory);
            }

            _logFileWriter = File.AppendText(logFilePath);
            _verboseLogging = isVerbose;
        }

        private string LogFormatter(string message)
        {
            string time = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
            message = $"[{time}]{message}";
            return message;
        }

        private void AddLogEntry(string message)
        {
            _logFileWriter.WriteLine(LogFormatter(message));
            _logFileWriter.Flush();
        }

        public void Info(string message)
        {
            message = $"[INFO] {message}";
            AddLogEntry(message);
        }

        public void Error(string message)
        {
            message = $"[*ERROR*] {message}";
            AddLogEntry(message);
        }

        public void Warn(string message)
        {
            message = $"[WARNING] {message}";
            AddLogEntry(message);
        }

        public void Debug(string message)
        {
            message = $"[DEBUG] {message}";
            AddLogEntry(message);
        }

        public void Verbose(string message)
        {
            if (!_verboseLogging) return;
            message = $"[VERBOSE] {message}";
            AddLogEntry(message);
        }

    }
}
