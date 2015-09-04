using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xen.Entity;

namespace ContactsManager.Domain.Entity
{
    public partial class Employee : BaseEFEntity
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Designation { get; set; }

    }
}
