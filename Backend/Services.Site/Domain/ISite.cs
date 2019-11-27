using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Sites.Domain
{
    public interface ISite : IDomain
    {
        ISite Setup(Guid businessId, string name, string postCode, string addressLine1, string addressLine2, IContact contact);

        void Update(string name, string postCode, string addressLine1, string addressLine2);

        Guid BusinessId { get; }

        string PostCode { get; }

        string AddressLine1 { get;  }

        string AddressLine2 { get;  }

        string Name { get; }

        Contact Contact { get; }

        IContact GetContact();
    }
}
