using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Sites.Domain;

namespace Services.Sites.Factories
{
    public interface ISiteServiceFactory
    {
        ISite CreateSite(Guid businessId, string name, string postCode, string addressLine1, string addressLine2, IContact contact);

        IContact CreateContact(string firstName, string secondName, string email, string number);
    }
}
