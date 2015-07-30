using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xen.Entity
{
    [DataContract]
    public class CrudResult : ICrudResult
    {
        [DataMember]
        public CrudStatusType CrudStatus { get; set; }

        [DataMember]
        public string AdditionalInfo { get; set; }

        public void AppendAdditionalInfo(string additionalMessage)
        {
            this.AdditionalInfo = !String.IsNullOrEmpty(this.AdditionalInfo)
                ? (this.AdditionalInfo + ", " + additionalMessage) :
            additionalMessage;
        }

        [DataMember]
        public string ValidationErrorMessage { get; set; }

        public string StatusMessage 
        {
            get { return "TODO"; }
        }

        [DataMember]
        public String ExceptionInfo { get; set; }

        public void PopulateExceptionInfo(Exception exception)
        {
            this.CrudStatus = CrudStatusType.ExceptionExists;
            this.AdditionalInfo = exception.Message;
            this.ExceptionInfo = exception.StackTrace;
        }
    }

    [DataContract]
    public class CrudResult<T> : CrudResult, ICrudResult<T>
    {
        [DataMember]
        public T Result { get; set; }

        [DataMember]
        public T OriginalEntity { get; set; }

        [DataMember]
        public int RowsAffected { get; set; }

        public void PopulateResultFrom(T result)
        {
            if (result.GetType().BaseType == typeof (IList<>))
            {
            }
        }

        public CrudResult()
        {
            this.Result = default(T);
            this.CrudStatus = CrudStatusType.Unknown;
        }
    }
}
