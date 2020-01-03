using System.Collections.Generic;

namespace Api.Gateway.Dtos.Sites
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
