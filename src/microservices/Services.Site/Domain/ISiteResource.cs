using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Sites.Domain
{
    public interface ISiteResource : IDomain
    {
        Guid SiteId { get; }
        string Name { get; }

        string Identifier { get; }

        ISiteResource Setup(Guid siteId, string name, string identifier);
    }
}
