using System.Diagnostics;

namespace Xen.Common.Services.Logging
{
    public class LoggerService : ILoggerService
    {
        public void Write(object message, string category = null, int priority = -1, int eventId = -1, TraceEventType eventType = TraceEventType.Information)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message, category, priority, eventId, eventType);
        }
    }
}
