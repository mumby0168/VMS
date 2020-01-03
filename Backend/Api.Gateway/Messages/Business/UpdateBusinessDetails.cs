using System;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Business
{
    [MicroService(Services.Common.Names.Services.Businesses)]
    public class UpdateBusinessDetails : ICommand
    {
        public Guid Id { get;}
        public string Name { get; }

        public string TradingName { get; }

        public string WebAddress { get; }

        [JsonConstructor]
        public UpdateBusinessDetails(Guid id, string name, string tradingName, string webAddress)
        {
            Id = id;
            Name = name;
            TradingName = tradingName;
            WebAddress = webAddress;
        }
    }
}
