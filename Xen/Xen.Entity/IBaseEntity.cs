using System;

namespace Xen.Entity
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        DateTime CreationTs { get; set; }
        string CreationUserId { get; set; }
        Nullable<DateTime> LastChangeTs { get; set; }
        string LastChangeUserId { get; set; }
        StatusType StatusType { get; set; }

        bool IsValid { get; }
        void Validate();        
    }
}
