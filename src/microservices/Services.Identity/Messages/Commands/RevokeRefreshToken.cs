using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Identity.Messages.Commands
{
    public class RevokeRefreshToken : ICommand
    {
        public string Token { get; }

        [JsonConstructor]
        public RevokeRefreshToken(string token)
        {
            Token = token;
        }
    }
}
