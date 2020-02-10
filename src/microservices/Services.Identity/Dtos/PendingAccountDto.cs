using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Dtos
{
    public class PendingAccountDto
    {
        public Guid Id { get; set; }

        public string EmailAddress { get; set; }
    }
}
