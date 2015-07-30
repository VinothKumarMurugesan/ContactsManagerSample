using System;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Xen.Common.Services.Logging
{
    internal static class XenLogger
    {
        private const string ExMessageFormat = "Message : {0}\nSTACK TRACE :{1}";
        private const string InnerExFormat = "\nINNER EXCEPTION : {0}\nINNER EXCEPTION STACK TRACE : {1}";
        //private const string InfoMessageFormat = "Module : {0}\nMessage : {1}";
        private static int eventId = 0;

        public static void Write(string module, Exception exception, string additionalMessage = "",
            TraceEventType severity = TraceEventType.Information)
        {
            var logEntry = new LogEntry
            {
                EventId = ++eventId,
                Severity = severity,
                Message = string.Format(ExMessageFormat, exception.Message, exception.StackTrace) + ((exception.InnerException != null) ?
                string.Format(InnerExFormat,
                    exception.InnerException.Message, exception.InnerException.StackTrace) : string.Empty)
            };
            logEntry.ExtendedProperties.Add("AdditionalMessage", additionalMessage);
            Write(module, logEntry);
        }

        public static void Write(string module, string message, TraceEventType severity = TraceEventType.Information)
        {
            var logEntry = new LogEntry
            {
                EventId = ++eventId,
                Severity = severity,
                Message = message
            };
            Write(module, logEntry);
        }

        private static void Write(string module, LogEntry logEntry)
        {
            var logWriter = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();
            logEntry.Title = module;
            logWriter.Write(logEntry);
        }
    }
}
