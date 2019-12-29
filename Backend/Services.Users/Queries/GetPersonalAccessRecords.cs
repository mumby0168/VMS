using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Users.Queries
{
    public class GetPersonalAccessRecords : IQuery
    {
        public Guid AccountId { get; set; }

        public GetPersonalAccessRecords(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
