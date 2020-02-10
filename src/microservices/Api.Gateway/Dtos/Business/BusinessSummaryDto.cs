using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Dtos.Business
{
    public class BusinessSummaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TradingName { get; set; }
        public int SiteCount { get; set; }
    }
}
