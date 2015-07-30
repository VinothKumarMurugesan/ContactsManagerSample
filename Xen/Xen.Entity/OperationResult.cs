using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Xen.Entity
{
    [DataContract]
    [KnownType(typeof(SqlErrorCollection))]
    [KnownType(typeof(SqlException))]
    [KnownType(typeof(UpdateException))]
    [KnownType(typeof(SqlError))]    
    
    public class OperationResult : IOperationResult
    {        
        public static Type[] GetKnownTypes() 
        { 
            List<Type> result = new List<Type>();
            result.Add(typeof(ArgumentException)); 
            result.Add(typeof(Dictionary<string, object>)); 
            result.Add(typeof(IDictionary).Assembly.GetType("System.Collections.ListDictionaryInternal")); 
            return result.ToArray(); 
        }
        [DataMember]
        public OperationStatusType OperationStatus { get; set; }

        [DataMember]
        public CrudStatusType CrudStatus { get; set; }

        public bool IsSuccessful
        {
            get { return (this.OperationStatus == OperationStatusType.Successful); }
        }

        [DataMember]
        public string AdditionalInfo { get; set; }

        public void AppendAdditionalInfo(string additionalMessage)
        {
            this.AdditionalInfo = !String.IsNullOrEmpty(this.AdditionalInfo)
                ? (this.AdditionalInfo + ", " + additionalMessage) :
            additionalMessage;
        }

        [DataMember]
        public string  ValidationErrorMessage { get; set; }

        public string StatusMessage 
        {
            get { return "TODO"; }
        }
        [DataMember] 
        public Exception OriginalException { get; set; }

        [DataMember]
        public String ExceptionInfo { get; private set; }

        [DataMember]
        public int ErrorTypeNumber { get; set; }

        public void PopulateExceptionInfo(Exception exception)
        {
            System.Data.SqlClient.SqlException _sqlException;
            this.OperationStatus = OperationStatusType.Failed;
            this.CrudStatus = CrudStatusType.ExceptionExists;

            //this.OriginalException = exception;           

            this.AdditionalInfo = exception.Message;
            if( exception.InnerException != null )
            {
                _sqlException = exception.InnerException as System.Data.SqlClient.SqlException;
                if (_sqlException != null)
                {   
                    this.ErrorTypeNumber = _sqlException.Number;
                }
                this.AdditionalInfo += string.Format("\nAdditional Information : {0}", exception.InnerException.Message);
            }
            else
            {
                _sqlException = exception as System.Data.SqlClient.SqlException;
                if (_sqlException != null)
                {
                    this.ErrorTypeNumber = _sqlException.Number;
                }
            }
            this.ExceptionInfo = exception.StackTrace;
            if (exception.InnerException != null)
            {
                this.ExceptionInfo += string.Format("\nInner Exception : {0}", exception.InnerException.StackTrace);
            }
        }

        public void PopulateResultFrom(IOperationResult result)
        {
            this.OperationStatus = result.OperationStatus;
            this.CrudStatus = result.CrudStatus;
            this.AdditionalInfo = result.AdditionalInfo;
        }
    }

    [DataContract]
    public class OperationResult<T> : OperationResult, IOperationResult<T>
    {
        [DataMember]
        public T Result { get; set; }

        [DataMember]
        public T OriginalEntity { get; set; }

        public OperationResult()
        {
            this.Result = default(T);
            this.OperationStatus = OperationStatusType.Unknown;
            this.CrudStatus = CrudStatusType.Unknown;
        }

        public void PopulateResultFrom(ICrudResult<T> icrudResult) 
        {
            if( icrudResult.CrudStatus == CrudStatusType.BussinesValidationNotSucceded)
            {
                this.CrudStatus = icrudResult.CrudStatus;
                this.OriginalEntity = icrudResult.OriginalEntity;
                this.Result = icrudResult.Result;
                this.ValidationErrorMessage = icrudResult.ValidationErrorMessage;
                this.OperationStatus = OperationStatusType.SuccessfulWithWarning;
            }
            else
            {
                this.CrudStatus = icrudResult.CrudStatus;
                this.OperationStatus = OperationStatusType.Successful;
                this.Result = icrudResult.Result;
                // If exception exists, need to show userdefinded error message to client - start
                this.AdditionalInfo = icrudResult.AdditionalInfo;
                // End
            }
        }
    }
}
