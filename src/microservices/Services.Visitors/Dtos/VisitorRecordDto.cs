using System;

namespace Services.Visitors.Dtos
{
    public class VisitorRecordDto
    {
        public Guid Id { get; set; }
        
        public string Action { get; set; }
        
        public string TimeStamp { get; set; }
        
        public string Date { get; set; }
        
        public Guid SiteId { get; set; }
        
        public string SiteName { get; set; }
    }
}