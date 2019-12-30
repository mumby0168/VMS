using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Factories
{
    public class SiteFactory : ISiteFactory
    {
        public ISite CreateSite(Guid id, string name)
        {
            return new Site(id, name);
        }
    }
}
