using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xen.Entity;

namespace ContactsManager.Domain.Entity
{
   public partial class Contact
    {
       [NotMapped]
       public string FullName
       {
           get { return string.Format("{0} - {1}", this.FirstName, this.LastName); }

       }

    }
}
