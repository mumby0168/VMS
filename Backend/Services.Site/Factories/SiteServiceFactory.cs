using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Sites.Domain;

namespace Services.Sites.Factories
{
    public class SiteServiceFactory : ISiteServiceFactory
    {
        public ISite CreateSite(Guid businessId, string name, string postCode, string addressLine1, string addressLine2, IContact contact)
        {
            return new Site().Setup(businessId, name, postCode, addressLine1, addressLine2, contact);
        }

        public IContact CreateContact(string firstName, string secondName, string email, string number)
        {
            return new Contact().Setup(firstName, secondName, email, number) ;
        }
    }
}
