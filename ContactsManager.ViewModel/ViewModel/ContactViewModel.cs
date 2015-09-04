using ContactsManager.Domain.Entity;
using ContactsManager.ViewModel.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xen.Entity;

namespace ContactsManager.ViewModel.ViewModel
{
    [FluentValidation.Attributes.Validator(typeof(ContactValidator))]
    public class ContactViewModel : BaseEFEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public bool IsActive { get; set; }
        public DateTime Born { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public Employee Employee { get; set; }
    }
}
