using System;

namespace Services.Users.Dtos
{
    public class AccessRecordDto
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public string SiteName { get; set; }

        public string Time { get; set; }

        public string Date { get; set; }
        public string Action { get; set; }
    }
}
