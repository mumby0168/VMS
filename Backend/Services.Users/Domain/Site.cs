using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Users.Domain
{
    public class Site : ISite
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Site(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
