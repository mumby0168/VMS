using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Test
{
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
