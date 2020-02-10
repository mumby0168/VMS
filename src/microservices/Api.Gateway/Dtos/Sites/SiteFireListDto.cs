using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Dtos.Sites
{
    public class SiteFireListDto
    {

        public SiteFireListDto()
        {
            Employees = new List<FireListUserDto>();    
        }

        public Guid SiteId { get; set; }

        public string Time { get; set; }

        public string Date { get; set; }

        public IEnumerable<FireListUserDto> Employees { get; set; }
    }
}
    