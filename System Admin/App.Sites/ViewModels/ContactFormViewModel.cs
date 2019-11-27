using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Sites.ViewModels
{
    public class ContactFormViewModel
    {
        public void Setup(Guid siteId, string firstName, string secondName, string email, string contactNumber)
        {
            Id = siteId;
            FirstName = firstName;
            SecondName = secondName;
            ContactNumber = contactNumber;
            Email = email;
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }

        public async Task SubmitAsync()
        {

        }
    }
}
