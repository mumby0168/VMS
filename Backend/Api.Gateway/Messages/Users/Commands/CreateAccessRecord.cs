using System;
using Api.Gateway.Messages.Users.Enums;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Users.Commands
{
    [MicroService(ServiceNames.Users)]
    public class CreateAccessRecord : ICommand
    {
        public Guid UserId { get; }

        public AccessAction Action { get; }

        [JsonConstructor]
        public CreateAccessRecord(Guid userId, AccessAction action)
        {
            UserId = userId;
            Action = action;
        }
    }
}
