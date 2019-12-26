using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Dtos.Users
{
    public class AccessRecordDto
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public string SiteName { get; set; }

        public DateTime TimeStamp { get; set; }
        public string Action { get; set; }
    }
}
