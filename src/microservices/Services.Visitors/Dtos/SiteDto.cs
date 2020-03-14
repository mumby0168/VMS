using System;

namespace Services.Visitors.Dtos
{
    public class SiteDto
    {
        public Guid Id { get; set; }

        public Guid BusinessId { get; set; }
        
        public string Name { get; set; }
    }

}
