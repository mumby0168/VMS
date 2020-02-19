using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Extensions;
using Services.Common.Validation;

namespace Services.Users.Domain
{
    public class User : IUser
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string BusinessPhoneNumber { get; private set; }
        public bool IsAccountValid { get; private set; }
        public Guid BasedSiteId { get; private set; }
        public Guid BusinessId { get; private set; }

        public int Code {get; private set;}

        public Guid AccountId { get; private set; }

        public void SuspendAccount()
        {
            IsAccountValid = false;
            AccountId = Guid.Empty;
        }

        public IUser Setup(string firstName, string secondName, string email, string phoneNumber, string businessPhoneNumber,
            Guid basedSiteId, Guid businessId, Guid accountId, int code) =>
            new User(firstName, secondName, email, phoneNumber, businessPhoneNumber, basedSiteId, businessId, accountId, code);

        public User()
        {
            
        }
        private User(string firstName, string secondName, string email, string phoneNumber, string businessPhoneNumber, Guid basedSiteId, Guid businessId, Guid accountId, int code)
        {

            if (firstName.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The field first name cannot be empty");
            if (secondName.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The field first name cannot be empty");
            if(!Validator.IsEmailValid(email)) throw new VmsException(Codes.InvalidEmail, $"The email {email} is not valid.");
            if (!Validator.IsUkPhoneNumberValid(phoneNumber)) throw new VmsException(Codes.InvalidContactNumber, $"The phone number {phoneNumber} is not valid.");
            if (!Validator.IsUkPhoneNumberValid(businessPhoneNumber)) throw new VmsException(Codes.InvalidContactNumber, $"The phone number {businessPhoneNumber} is not valid.");



            Id = Guid.NewGuid();
            FirstName = firstName;
            Code = code;
            SecondName = secondName;
            Email = email;
            PhoneNumber = phoneNumber;
            BusinessPhoneNumber = businessPhoneNumber;
            BasedSiteId = basedSiteId;
            BusinessId = businessId;
            AccountId = accountId;
            IsAccountValid = true;
        }
    }
}
