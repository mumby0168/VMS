using System.Collections.Generic;

namespace Services.Users.Dtos
{
    public class SiteAccessDetailsDto
    {
        public SiteAccessDetailsDto()
        {
            UserRecords = new List<UserAccessDetailsDto>();
        }

        public string SiteName { get; set; }
        public IEnumerable<UserAccessDetailsDto> UserRecords { get; set; }
    }
}
    