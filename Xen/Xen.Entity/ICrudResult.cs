using System;
using System.Runtime.Serialization;

namespace Xen.Entity
{
    public interface ICrudResult
    {
        [DataMember]
        string ValidationErrorMessage { get; set; }

        [DataMember]
        string AdditionalInfo { get; set; }

        [DataMember]
        CrudStatusType CrudStatus { get; set; }

        void AppendAdditionalInfo(string additionalMessage);

        string StatusMessage { get; }

        [DataMember]
        String ExceptionInfo { get;  }

        void PopulateExceptionInfo(Exception exception);
    }

    public interface ICrudResult<T> : ICrudResult
    {
        [DataMember]
        T Result { get; set; }

        [DataMember]
        T OriginalEntity { get; set; }

        [DataMember]
        int RowsAffected { get; set; }

        void PopulateResultFrom( T result );
    }
}
