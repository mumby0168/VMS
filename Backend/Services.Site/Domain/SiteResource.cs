using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Extensions;

namespace Services.Sites.Domain
{
    public class SiteResource : ISiteResource
    {
        public Guid Id { get; private set; }
        public Guid SiteId { get; private set; }
        public string Name { get; private set; }
        public string Identifier { get; private set; }

        public ISiteResource Setup(Guid siteId, string name, string identifier)
        {
            if(name.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The field name is required.");
            if(identifier.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The field identifier is required.");

            Id = Guid.NewGuid();
            SiteId = siteId;
            Name = name;
            Identifier = identifier;
            return this;
        }
    }
}
