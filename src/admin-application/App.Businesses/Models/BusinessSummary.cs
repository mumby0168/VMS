using System;
using System.Collections.Generic;
using System.Text;

namespace App.Businesses.Models
{
    public class BusinessSummary
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string TradingName { get; set; }
        public int SiteCount { get; set; }
    }
}
    