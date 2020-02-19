using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;
using Services.Users.Domain;

namespace Services.Users.Commands
{
    public class CreateAccessRecord : ICommand
    {
        public int Code { get; }

        public AccessAction Action { get; }

        [JsonConstructor]
        public CreateAccessRecord(int code, AccessAction action)
        {
            Code = code;
            Action = action;
        }
    }
}
