using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Dtos
{
    public class StandardUserAccountDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
