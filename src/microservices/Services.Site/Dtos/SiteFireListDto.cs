using System;
using System.Collections.Generic;

namespace Services.Sites.Dtos
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
    