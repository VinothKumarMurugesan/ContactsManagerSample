using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Script.Serialization;

namespace Xen.Entity
{
    [JsonObject(IsReference = true)]
    //[ScriptIgnore]
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            this.Id = default(long);
        }

        [Key]
        public virtual long Id { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime CreationTs { get; set; }

        
        [MaxLength(50)]
        [DataMember(IsRequired = true)]
        public string CreationUserId { get; set; }

        public virtual Nullable<DateTime> LastChangeTs { get; set; }

        [MaxLength(50)]
        public virtual string LastChangeUserId { get; set; }

        [DataMember(IsRequired = true)]
        public virtual StatusType StatusType { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 23 + this.Id.GetHashCode();
                result = result * 23 + this.CreationTs.GetHashCode();
                result = result * 23 + ((CreationUserId != null) ? this.CreationUserId.GetHashCode() : 0);
                result = result * 23 + ((LastChangeTs != null) ? this.LastChangeTs.GetHashCode() : 0);
                result = result * 23 + ((LastChangeUserId != null) ? this.LastChangeUserId.GetHashCode() : 0);
                result = result * 23 + this.StatusType.GetHashCode();
                result = result * 23 + this.State.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            var tempObj = obj as BaseEntity;
            if (tempObj != null && obj.GetType() == this.GetType())
            {
                return (tempObj.UniqueId == this.UniqueId);
            }
            else
            {
                return false;
            }
        }

        [NotMapped]
        public virtual string Error
        {
            get { return string.Empty; }
        }

        protected long UniqueId
        {
            get { return (this.Id == 0 ? base.GetHashCode() : this.Id);  }
        }

        //public override int GetHashCode()
        //{
        //    return (this.Id == 0 ? base.GetHashCode() : this.Id);
        //}

        public virtual bool IsValid
        {
            get;
           private set;

        }

        public virtual void Validate()
        {

        }

        public virtual bool CanDelete
        {
            get { return this.Id != default(int); }
        }

        public virtual bool IsDirty
        {
            get { return false; }
        }

        public virtual bool IsAddOrModify
        {
            get { return false; }
        }

        [NotMapped]
        public virtual EntityState State { get; set; }

        public virtual T Clone<T>()
        {
            using (var outStream = new MemoryStream())
            {
                var dataContractSerializer = new DataContractSerializer(typeof (T));
                dataContractSerializer.WriteObject(outStream, this);

                if (outStream.Length <= 0)
                    return default(T);

                string str = Encoding.ASCII.GetString(outStream.ToArray());
                byte[] bytes = Encoding.ASCII.GetBytes(str);

                using (var inStream = new MemoryStream(bytes))
                {
                    var dataContractDeserializer = new DataContractSerializer(typeof(T));
                    return (T)dataContractDeserializer.ReadObject(inStream);
                }
            }
        }      

    }
}
