using RabbitMQ.Client;

namespace Services.RabbitMq.Interfaces.Factories
{
    public interface IServiceBusConnectionFactory
    {
        IServiceBusConnection ResolveServiceBusConnection();

        ConnectionFactory CreateConnectionFactory(string hostName);
    }
}
