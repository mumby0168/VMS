using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Dtos.Users
{
    public class UserAccessDetailsDto
    {
        public UserAccessDetailsDto()
        {
            Records = new List<AccessRecordDto>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<AccessRecordDto> Records { get; set; }
    }
}
