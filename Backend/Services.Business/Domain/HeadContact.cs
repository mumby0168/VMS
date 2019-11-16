using Services.Common.Exceptions;
using Services.Common.Extensions;
using Validator = Services.Common.Validation.Validator;

namespace Services.Business.Domain
{
    public class HeadContact
    {
        public string FirstName { get; private set; }

        public string SecondName { get; private set; }

        public string ContactNumber { get; private set; }

        public string Email { get; private set; }

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
