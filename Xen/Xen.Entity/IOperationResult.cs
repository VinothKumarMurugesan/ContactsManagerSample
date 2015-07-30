using System;
using System.Runtime.Serialization;

namespace Xen.Entity
{   
    public interface IOperationResult
    {
        [DataMember]
        OperationStatusType OperationStatus { get; set; }

        [DataMember]
        string ValidationErrorMessage { get; set; }

        [DataMember]
        string AdditionalInfo { get; set; }

        [DataMember]
        CrudStatusType CrudStatus { get; set; }

        bool IsSuccessful { get; }

        void AppendAdditionalInfo(string additionalMessage);

        string StatusMessage { get; }

        [DataMember]
        String ExceptionInfo { get; }

        [DataMember]
        int ErrorTypeNumber { get; set; }

        [DataMember]
        Exception OriginalException { get; set; }
        
        void PopulateExceptionInfo(Exception exception);
        void PopulateResultFrom(IOperationResult result);
    }

    public interface IOperationResult<T> : IOperationResult
    {
        [DataMember]
        T Result { get; set; }

        [DataMember]
        T OriginalEntity { get; set; }

        void PopulateResultFrom(ICrudResult<T> icrudResult);
    }
}
