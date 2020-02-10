using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Sites.Dtos
{
    public class SiteUserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }

        public DateTime TimeStamp { get; set; }
        public string Initials { get; set; }
    }
}
