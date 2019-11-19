using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Test
{
    [MicroService(ServiceNames.Test)]
    public class IssueCommand : ICommand
    {
        public string Message { get; }

        [JsonConstructor]
        public IssueCommand(string message)
        {
            Message = message;
        }
    }
}
