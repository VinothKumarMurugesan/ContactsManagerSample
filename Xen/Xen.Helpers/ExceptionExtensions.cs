using System;

namespace Xen.Helpers
{
    public static class ExceptionExtensions
    {
        public static string GetFormattedExceptionMessage(this Exception exception, bool includeHeader=false)
        {
            string message = (includeHeader ? "Exception:" : string.Empty);
            //message += exception.Message;

            var rootException = exception.GetBaseException();

            if (rootException != null )
            {
                //message += string.Format(". Additional Information : {0}", rootException.Message);
                message += string.Format(" Additional Information : {0}", rootException.Message);
            }
            return message;
        }

       
    }
}
