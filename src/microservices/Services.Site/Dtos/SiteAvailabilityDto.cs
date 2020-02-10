using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Sites.Dtos
{
    public class SiteAvailabilityDto
    {
        public SiteAvailabilityDto()
        {
            Users = new List<SiteUserDto>();
        }

        public string SiteName { get; set; }

        public IEnumerable<SiteUserDto> Users { get; set; }
    }
}
