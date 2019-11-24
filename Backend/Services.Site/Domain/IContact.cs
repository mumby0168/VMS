using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Sites.Domain
{
    public interface IContact : IDomain
    {
        IContact Setup(string firstName, string secondName, string email, string number);

        string FirstName { get; }


        string SecondName { get; }


        string ContactNumber { get; }


        string Email { get; }
    }
}
