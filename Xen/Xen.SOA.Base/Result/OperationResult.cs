using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Unify.SOA.Base
{
    public class OperationResult : IOperationResult
    {
        [DataMember]
        public OperationStatusType OperationStatus { get; set; }

        [DataMember]
        public CRUDStatusType CRUDStatus { get; set; }

        public bool IsSuccessful
        {
            get { return (OperationStatus == OperationStatusType.Successful); }
        }

        [DataMember]
        public string AdditionalInfo { get; set; }

        public void AppendAdditionalInfo(string additionalMessage)
        {
            this.AdditionalInfo = !String.IsNullOrEmpty(this.AdditionalInfo)
                ? (this.AdditionalInfo + "," + additionalMessage) :
            additionalMessage;
        }

        [DataMember]
        public IList<string> ValidationErrorMessage { get; set; }

        public string StatusMessage 
        {
            get { return "TODO"; }
        }
    }

    public class OperationResult<T> : OperationResult, IOperationResult<T>
    {
        [DataMember]
        public T Result { get; set; }
    }
}
