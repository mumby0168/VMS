using System;

namespace Services.Sites.Dtos
{
    public class LatestAccessRecordDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Action { get; set; }

        private Guid SiteId { get; set; }

        public DateTime TimeStamp { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string Initials { get; set; }
    }
}
