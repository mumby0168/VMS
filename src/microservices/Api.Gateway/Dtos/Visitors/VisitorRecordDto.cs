using System;

namespace Api.Gateway.Dtos.Visitors
{
    public class VisitorRecordDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string InTime { get; set; }
        
        public string OutTime { get; set; }
        
        public string Date { get; set; }
        
        public Guid SiteId { get; set; }
        
        public string SiteName { get; set; }
    }
}