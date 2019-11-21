using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Services.Identity.Messages.Commands
{
    public class DeleteBusinessAdmin
    {
        public Guid Id { get;  }

        public Guid BusinessId { get; }

        [JsonConstructor]
        public DeleteBusinessAdmin(Guid id, Guid businessId)
        {
            Id = id;
            BusinessId = businessId;
        }
    }
}
