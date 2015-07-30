

namespace Xen.Entity
{
    public enum CrudStatusType : short
    {
        Unknown = 0,

        DataAddedSuccessfully = 1,
        DataDeletedSuccessfully = 2,
        DataUpdatedSuccessfully = 3,
        DataSelectedSuccessfully = 4,

        DataNotAdded = 5,
        DataNotDeleted = 6,
        DataNotUpdated = 7,

        DataDoesNotExist = 8, 
        DataAlreadyExists = 9,

        DataIsInUse = 10,
        DataIsNotInUse = 11,

        DataIsNotValid = 12,
        DataIsValid = 13,

        BussinesValidationNotSucceded = 14,

        DatabaseOperationFailed = 15,
        ExceptionExists = 16,

        DataQueuedSuccessfully = 17,
        DataNotQueued = 18,

        InvalidUserPassword = 19,
        InvalidUserContext = 20,
        ValidUserContext = 21,

        NoRecordFound = 22,
    }

    public enum MessageNotificationType : byte
    {
        None = 0,
        TextEMail = 1,
        HtmlEMail = 2,
        SMS = 3,
        Pager = 4,
        IM = 5
    }

    public enum ModemType : byte
    {
        None = 0,
        Common = 1,
        Wiman = 2,
        SmsApi = 3
    }
    
    public enum StatusType : short
    {
        Disabled = 0,
        Enabled = 1,
    }
}
