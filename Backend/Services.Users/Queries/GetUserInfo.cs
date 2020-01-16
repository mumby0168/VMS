using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Queries;

namespace Services.Users.Queries
{
    public class GetUserInfo : IQuery
    {
        public Guid AccountId { get; }

        [JsonConstructor]
        public GetUserInfo(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
