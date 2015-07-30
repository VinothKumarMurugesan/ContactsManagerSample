using System.Diagnostics;

namespace Xen.Common.Services.Logging
{
    public interface ILoggerService
    {
        void Write(object message, string category = null, int priority = -1, int eventId = -1, TraceEventType eventType = TraceEventType.Information);
    }
}
