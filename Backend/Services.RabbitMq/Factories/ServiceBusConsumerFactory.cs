using System.Diagnostics.CodeAnalysis;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.RabbitMq.Interfaces.Factories;

namespace Services.RabbitMq.Factories
{
    [ExcludeFromCodeCoverage]
    public class ServiceBusConsumerFactory : IServiceBusConsumerFactory
    {
        public EventingBasicConsumer CreateBasicConsumer(IModel model)
        {
            return new EventingBasicConsumer(model);
        }
    }
}
