using System;
using System.Collections.Generic;

namespace Services.Users.Dtos
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
