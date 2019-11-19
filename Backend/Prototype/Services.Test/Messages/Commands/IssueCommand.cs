using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Test.Messages.Commands
{
    [MicroService(ServiceNames.Gateway)]
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
