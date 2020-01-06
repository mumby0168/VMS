using System;

namespace Services.Sites.Dtos
{
    public class OnSiteAccessRecordDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FullName { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
