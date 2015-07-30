using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Unify.SOA.Base
{
    public interface IOperationResult
    {
        [DataMember]
        OperationStatusType OperationStatus { get; set; }

        [DataMember]
        IList<string> ValidationErrorMessage { get; set; }

        [DataMember]
        string AdditionalInfo { get; set; }

        [DataMember]
        CRUDStatusType CRUDStatus { get; set; }

        bool IsSuccessful { get; }

        void AppendAdditionalInfo(string additionalMessage);

        string StatusMessage { get; }
    }

    public interface IOperationResult<T> : IOperationResult
    {
        [DataMember]
        T Result { get; set; }
    }
}
