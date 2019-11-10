using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Services.Common.Exceptions;
using Services.Common.Extensions;
using Validator = Services.Common.Validation.Validator;

namespace Services.Businesses.Domain
{
    public class HeadContact
    {
        public string FirstName { get; }

        public string SecondName { get; }

        public string ContactNumber { get; }

        public string Email { get; }

        public HeadContact(string firstName, string secondName, string contactNumber, string email)
        {
            if(firstName.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The first name cannot be empty");
            if (secondName.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The second name cannot be empty");
            if (!Validator.IsUkPhoneNumberValid(contactNumber)) throw new VmsException(Codes.InvalidContactNumber, $"The contact number {contactNumber} is invalid");
            if(!Validator.IsEmailValid(email)) throw new VmsException(Codes.InvalidEmail, $"The email {email} is invalid");

            FirstName = firstName;
            SecondName = secondName;
            ContactNumber = contactNumber;
            Email = email;
        }
    }
}
