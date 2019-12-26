using System;

namespace Services.Users.Dtos
{
    public class AccessRecordDto
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public string SiteName { get; set; }

        public DateTime TimeStamp { get; set; }
        public string Action { get; set; }
    }
}
