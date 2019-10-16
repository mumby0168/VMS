using Services.RabbitMq.Exchange;
using Services.RabbitMq.Factories;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Connection;
using Services.RabbitMq.Interfaces.Exchange;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Interfaces.Queues;
using Services.RabbitMq.Interfaces.Settings;
using Services.RabbitMq.Managers;
using Services.RabbitMq.Messages;
using Services.RabbitMq.Queue;
using Services.RabbitMq.Settings;

namespace Services.RabbitMq
{
    public static class RabbitMqConfig
    {
        //public static void ConfigureDi(ContainerBuilder containerBuilder)
        //{
        //    containerBuilder.RegisterType<ServiceBusConnection>().As<IServiceBusConnection>().SingleInstance();
        //    containerBuilder.RegisterType<ServiceBusQueueFactory>().As<IServiceBusQueueFactory>();
        //    containerBuilder.RegisterType<ServiceBusExchangeFactory>().As<IServiceBusExchangeFactory>();
        //    containerBuilder.RegisterType<ServiceBusQueue>().As<IServiceBusQueue>();
        //    containerBuilder.RegisterType<ServiceBusExchange>().As<IServiceBusExchange>();
        //    containerBuilder.RegisterType<ServiceBusMessageHandler>().As<IServiceBusMessageHandler>();
        //    containerBuilder.RegisterType<ServiceBusMessageSubscriber>().As<IServiceBusMessageSubscriber>().SingleInstance();
        //    containerBuilder.RegisterType<ServiceBusManager>().As<IServiceBusManager>();
        //    containerBuilder.RegisterType<ServiceBusMessagePublisher>().As<IServiceBusMessagePublisher>();
        //    containerBuilder.RegisterType<ServiceBusConnectionFactory>().As<IServiceBusConnectionFactory>();
        //    containerBuilder.RegisterType<ServiceSettings>().As<IServiceSettings>().SingleInstance();
        //    containerBuilder.RegisterType<ServiceBusMessageHandler>().As<IServiceBusMessageHandler>();
        //    containerBuilder.RegisterType<ServiceBusConsumerFactory>().As<IServiceBusConsumerFactory>();
        //    containerBuilder.RegisterType<HandlerFactory>().As<IHandlerFactory>();
        //}

    }
}
