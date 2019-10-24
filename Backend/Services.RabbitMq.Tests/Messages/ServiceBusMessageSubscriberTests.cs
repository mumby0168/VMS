using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;

namespace Services.RabbitMq.Tests.Messages
{
    class ServiceBusMessageSubscriberTests
    {
        private Mock<IServiceBusConnectionFactory> _connectionFactory;
        private Mock<IServiceBusConnection> _connection;
        private Mock<IModel> _model;
        private Mock<IServiceBusMessageHandler> _handler;
        private Mock<IServiceBusConsumerFactory> _factory;
        private Mock<IServiceBusMessage> _message;
        private const string TestServiceName = "Services.Test";
        private Mock<EventingBasicConsumer> _consumer;


        [SetUp]
        public void Setup()
        {
            _model = new Mock<IModel>();
            _connection = new Mock<IServiceBusConnection>();
            _connectionFactory = new Mock<IServiceBusConnectionFactory>();
            _connection.SetupGet(o => o.Channel).Returns(_model.Object);
            _connectionFactory.Setup(o => o.ResolveServiceBusConnection()).Returns(_connection.Object);
            _handler = new Mock<IServiceBusMessageHandler>();
            _factory = new Mock<IServiceBusConsumerFactory>();
            _message = new Mock<IServiceBusMessage>();
            _consumer = new Mock<EventingBasicConsumer>(null);
            _factory.Setup(o => o.CreateBasicConsumer(It.IsAny<IModel>()))
                .Returns(_consumer.Object);
        }

        [Test]
        public void Subscribe_Always_CreatesABasicConsume()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            sut.Subscribe<IServiceBusMessage>(TestServiceName, Callback);

            //Assert
            _model.Verify(o => o.BasicConsume(TestServiceName, true, It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>(), _consumer.Object));
        }

        private Task Callback(IServiceBusMessage message, IRequestInfo requestInfo) => Task.CompletedTask;

        
        public ServiceBusMessageSubscriber CreateSut() => new ServiceBusMessageSubscriber(_connectionFactory.Object, _handler.Object, _factory.Object);
    }
}
