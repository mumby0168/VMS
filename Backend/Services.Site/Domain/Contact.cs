using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;
using Services.Common.Exceptions;
using Services.Common.Extensions;
using Services.Common.Validation;

namespace Services.Sites.Domain
{
    public class Contact : IContact
    {
        public IContact Setup(string firstName, string secondName, string email, string number)
        {

            if (firstName.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The first name cannot be empty");
            if (secondName.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The second name cannot be empty"); if (!Validator.IsUkPhoneNumberValid(number)) throw new VmsException(Codes.InvalidContactNumber, $"The contact number {number} is invalid");
            if (!Validator.IsEmailValid(email)) throw new VmsException(Codes.InvalidEmail, $"The email {email} is invalid");


            Id = Guid.NewGuid();
            FirstName = firstName;
            SecondName = secondName;
            Email = email;
            ContactNumber = number;

            return this;
        }

        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string ContactNumber { get; private set; }
        public string Email { get; private set; }
        public Guid Id { get; private set; }
    }
}
