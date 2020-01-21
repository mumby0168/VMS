using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Dtos.Users
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
