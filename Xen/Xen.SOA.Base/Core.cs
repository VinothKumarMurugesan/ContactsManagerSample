using System;
using System.Diagnostics;
using Xen.Common.Services.Logging;
using Xen.Entity;

namespace Xen.SOA.Base
{
    public class Core
    {
        public static void HandleException(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException();
            }
            XenLogger.Write(exception);
        }

        public static void HandleException(Exception exception,string additionalMessage)
        {
            if (exception == null)
            {
                throw new ArgumentNullException();
            }
            XenLogger.Write(exception, additionalMessage);
        }

        public static void HandleSoaException(Exception exception, IOperationResult operationResult)
        {
            if (exception == null)
            {
                throw new ArgumentNullException();
            }
            if (operationResult == null)
            {
                throw new ArgumentNullException();
            }
            operationResult.PopulateExceptionInfo(exception);
            XenLogger.Write(exception);
        }

        public virtual void HandleException(Exception exception, IOperationResult operationResult)
        {
            if (exception == null)
            {
                throw new ArgumentNullException();
            }
            if (operationResult == null)
            {
                throw new ArgumentNullException();
            }
            operationResult.PopulateExceptionInfo(exception);
            XenLogger.Write(exception);
        }

        public virtual void HandleException(Exception exception, ICrudResult crudResult)
        {
            if (exception == null)
            {
                throw new ArgumentNullException();
            }
            if (crudResult == null)
            {
                throw new ArgumentNullException();
            }
            crudResult.PopulateExceptionInfo(exception);
            XenLogger.Write(exception);
        }

        public static void LogError(string message, string pageName)
        {
            XenLogger.Write(pageName, message, TraceEventType.Error);
        }

        public static void LogWarning(string message, string pageName)
        {
            XenLogger.Write(pageName, message, TraceEventType.Warning);
        }

        public static void LogInformation(string message, string pageName)
        {
            XenLogger.Write(pageName, message, TraceEventType.Information);
        }

        public static void LogMethodCalledInformation(
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName="")
        {
            XenLogger.Write(string.Format( "{0}::{1}",  methodName.ToUpper() , fileName  ), "Method Called", TraceEventType.Information);
        }

        public static void LogException(Exception exception, string pageName)
        {
            XenLogger.Write(exception, pageName, TraceEventType.Error);
        }
    }
}
