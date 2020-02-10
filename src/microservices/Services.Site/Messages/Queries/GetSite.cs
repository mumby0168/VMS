using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Sites.Messages.Queries
{
    public class GetSite : IQuery
    {
        public Guid Id { get; }

        public GetSite(Guid id)
        {
            Id = id;
        }
    }
}
