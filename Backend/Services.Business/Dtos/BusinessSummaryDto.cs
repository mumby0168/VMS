using System;

namespace Services.Business.Dtos
{
    public class BusinessSummaryDto
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TradingName { get; set; }
        public int SiteCount { get; set; }
    }
}
