using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Core.Operations;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Users.Handlers.Events
{
    public class CreateUserRejected : IRejectedEvent
    {
        public string Code { get; }
        public string Reason { get; }


        public CreateUserRejected(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }
    }
}
