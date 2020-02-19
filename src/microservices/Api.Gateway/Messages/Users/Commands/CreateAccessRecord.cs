using System;
using Api.Gateway.Messages.Users.Enums;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Users.Commands
{
    [MicroService(Services.Common.Names.Services.Users)]
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
