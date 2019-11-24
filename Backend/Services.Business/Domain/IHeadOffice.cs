using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Business.Domain
{
    public interface IHeadOffice
    {
        IHeadOffice Setup(string postCode, string addressLine1, string addressLine2);
        void Update(string postCode, string addressLine1, string addressLine2);

        string PostCode { get;  }

        string AddressLine1 { get; }

        string AddressLine2 { get; }
    }
}
