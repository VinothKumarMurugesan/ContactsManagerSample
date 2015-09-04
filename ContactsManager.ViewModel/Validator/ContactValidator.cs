using FluentValidation;
using ContactsManager.Domain.Entity;
using ContactsManager.Resource;
using ContactsManager.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.ViewModel.Validator
{
    public class ContactValidator : AbstractValidator<ContactViewModel>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(string.Format(ErrorMessage.Required, ContactsManager.Resource.Properties.Resource.FirstName))
                 .Length(0, 200).WithMessage(string.Format(ContactsManager.Resource.ErrorMessage.MaxLength, ContactsManager.Resource.Properties.Resource.FirstName, 200));
                

            RuleFor(x => x.LastName).NotEmpty().WithMessage(string.Format(ErrorMessage.Required, ContactsManager.Resource.Properties.Resource.LastName))
                .Length(0, 200).WithMessage(string.Format(ContactsManager.Resource.ErrorMessage.MaxLength, ContactsManager.Resource.Properties.Resource.LastName, 200));


            RuleFor(x => x.Email).NotEmpty().WithMessage(string.Format(ErrorMessage.Required, Resource.Properties.Resource.Email))
                .EmailAddress().WithMessage(string.Format(ContactsManager.Resource.ErrorMessage.Invalid, Resource.Properties.Resource.Email));

            RuleFor(x => x.Cellphone).NotEmpty().WithMessage(string.Format(ErrorMessage.Required, Resource.Properties.Resource.Cellphone));

            RuleFor(x => x.Born).Must(IsValidDate).WithMessage(string.Format(ErrorMessage.Required, Resource.Properties.Resource.Born));

        }

        private bool IsValidDate(DateTime date)
        {
            if (date == default(DateTime))
            {
                return false;
            }
            return true;
        }
    }
}
