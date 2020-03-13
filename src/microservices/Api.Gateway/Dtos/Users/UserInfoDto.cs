using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Dtos.Users
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public Guid BasedSiteId { get; set; }
        
        public string Code { get; set; }
    }
}
