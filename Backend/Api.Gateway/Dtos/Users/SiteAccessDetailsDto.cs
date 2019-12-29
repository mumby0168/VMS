using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Dtos.Users
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
    