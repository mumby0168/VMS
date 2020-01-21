using System;

namespace Services.Users.Dtos
{
    public class UserSnapshotDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Initials { get; set; }

        public string LastAction { get; set; }

        public string LastTime { get; set; }

        public string SiteName { get; set; }
    }
}
