using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Services.RabbitMq.Interfaces.Factories
{
    public interface IServiceBusConsumerFactory
    {
        EventingBasicConsumer CreateBasicConsumer(IModel model);
    }
}
