using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Identity.Messages.Query
{
    public class GetAllBusinessAdmins : IQuery
    {
        public Guid BusinessId { get; }

        public GetAllBusinessAdmins(Guid businessId)
        {
            BusinessId = businessId;
        }
    }
}
