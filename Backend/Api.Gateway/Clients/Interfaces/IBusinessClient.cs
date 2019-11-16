using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Dtos.Business;

namespace Api.Gateway.Clients.Interfaces
{
    public interface IBusinessClient
    {
        Task<IEnumerable<BusinessSummaryDto>> GetBusinessSummaries();
    }
}
