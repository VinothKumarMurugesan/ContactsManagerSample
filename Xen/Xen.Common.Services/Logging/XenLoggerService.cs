using System;
using System.Diagnostics;
using Xen.Entity;

namespace Xen.Common.Services.Logging
{
    public class XenLoggerService
    {
        public static void HandleException(Exception exception,
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName = "")
        {
            if (exception == null)
            {
                throw new ArgumentNullException();
            }
            XenLogger.Write(string.Format( "{0}::{1}",  methodName.ToUpper() , fileName  ), exception);
        }

        public static void HandleException(Exception exception, string additionalMessage,
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName = "")
        {
            if (exception == null)
            {
                throw new ArgumentNullException();
            }
            XenLogger.Write(string.Format( "{0}::{1}",  methodName.ToUpper() , fileName  ), exception, additionalMessage);
        }

        public static void HandleSoaException(Exception exception, IOperationResult operationResult,
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName = "")
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
            XenLogger.Write(string.Format( "{0}::{1}",  methodName.ToUpper() , fileName  ), exception);
        }

        public virtual void HandleException(Exception exception, IOperationResult operationResult,
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName = "")
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
            XenLogger.Write(string.Format( "{0}::{1}",  methodName.ToUpper() , fileName  ), exception);
        }

        public virtual void HandleException(Exception exception, ICrudResult crudResult,
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName="")

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
            XenLogger.Write(string.Format( "{0}::{1}",  methodName.ToUpper() , fileName  ), exception);
        }

        public static void LogError(string message,
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName = "")
        {
            XenLogger.Write(string.Format("{0}::{1}", methodName.ToUpper(), fileName), message, TraceEventType.Error);
        }

        public static void LogWarning(string message,
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName = "")
        {
            XenLogger.Write(string.Format("{0}::{1}", methodName.ToUpper(), fileName), message, TraceEventType.Warning);
        }

        public static void LogInformation(string message,
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName = "")
        {
            XenLogger.Write(string.Format("{0}::{1}", methodName.ToUpper(), fileName), message, TraceEventType.Information);
        }

        public static void LogMethodCalledInformation(
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName="")
        {
            XenLogger.Write(string.Format( "{0}::{1}",  methodName.ToUpper() , fileName  ), "Method Called", TraceEventType.Information);
        }

        public static void LogException(Exception exception, 
            [System.Runtime.CompilerServices.CallerFilePath]string fileName = "",
            [System.Runtime.CompilerServices.CallerMemberName]string methodName = "")
        {
            XenLogger.Write(string.Format("{0}::{1}", methodName.ToUpper(), fileName), exception, string.Empty, TraceEventType.Error);
        }
    }
}
