using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Sites.Domain
{
    public class Business : IBusiness
    {

        public Guid Id { get; private set; }
        public IBusiness Create(Guid id)
        {
            Id = id;
            return this;
        }
    }
}
