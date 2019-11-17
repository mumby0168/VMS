using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Queries;

namespace Services.Business.Messages.Queries
{
    public class GetBusiness : IQuery
    {
        public Guid Id { get; set; }
    }   
}
