using System;

namespace Api.Gateway.Dtos.Users
{
    public class LatestAccessRecordDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Action { get; set; }

        public string Code {get; set; }

        private Guid SiteId { get; set; }

        public DateTime TimeStamp { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }
        public string Initials { get; set; }
    }
}
