using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using FluentValidation.Results;
using FluentValidation.Attributes;
using FluentValidation;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Xen.Entity
{
    [JsonObject(IsReference = true)]
    public class BaseEFEntity : BaseEntity, ICloneable
    {

        public BaseEFEntity Clone() { return (BaseEFEntity)this.MemberwiseClone(); }
        object ICloneable.Clone() { return Clone(); }

        [NotMapped]
        public IList<ValidationFailure> ErrorList
        {
            get
            {
                var customAttributes = this.GetType().GetCustomAttributes(typeof(ValidatorAttribute), true);
                if (customAttributes.Length == 0)
                    return new List<ValidationFailure>();
                ValidatorAttribute validatorAttribute = customAttributes[0] as ValidatorAttribute;
                if (validatorAttribute == null)
                    return new List<ValidationFailure>();
                IValidator validator = Activator.CreateInstance(validatorAttribute.ValidatorType) as IValidator;
                FluentValidation.Results.ValidationResult result = validator.Validate(this);
                return result.Errors;
                
            }
        }

        [NotMapped]
        public virtual bool IsNewRecord
        {
            get { return (this.Id == 0); }
        }

        public override bool IsValid
        {
            get
            {
                return (this.ErrorList.Count == 0);
            }
        }

        //[NotMapped]
        //public virtual bool IsValid
        //{
        //    get { return (this.ErrorList.Count == 0); }
        //}
    }

    public struct TableIdPair
    {
        public string TableName;
        public long Id;
    }
}
