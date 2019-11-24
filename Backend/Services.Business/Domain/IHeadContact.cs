using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Business.Domain
{
    public interface IHeadContact
    {
        string FirstName { get;  }

        string SecondName { get;  }

        string ContactNumber { get; }


        string Email { get; }

        IHeadContact Setup(string firstName, string secondName, string contactNumber, string email);

        void Update(string firstName, string secondName, string contactNumber, string email);
    }
}
