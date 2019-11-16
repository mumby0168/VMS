using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RabbitMQ.Client;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Exchange;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Interfaces.Queues;
using Services.RabbitMq.Interfaces.Settings;
using Services.RabbitMq.Managers;
using Services.RabbitMq.Messages;
using Services.Tests.Mocks;
using Services.Tests.Mocks.Messaging;
using Shouldly;

namespace Services.RabbitMq.Tests
{
    public class ServiceBusManagerTests
    {
        private Mock<IServiceBusConnection> _serviceConnection;
        private Mock<IHandlerFactory> _handlerFactory;
        private Mock<IServiceBusExchangeFactory> _exchangeFactory;
        private Mock<IServiceBusQueueFactory> _queueFactory;
        private Mock<IServiceBusMessageSubscriber> _subscriber;
        private Mock<IServiceSettings> _settings;
        private Mock<IServiceBusConnectionFactory> _serviceBusConnectionFactory;
        private Mock<ConnectionFactory> _connectionFactory;
        private Mock<IConnection> _rabbitConnection;
        private Mock<IServiceBusSettings> _busSettings;
        private Mock<IServiceSettings> _serviceSettings;
        private Mock<IServiceBusExchange> _exchange;
        private Mock<IServiceBusQueue> _queue;
        private Mock<MockCommandHandler> _commandHandler;
        private Mock<MockEventHandler> _mockEventHandler;
        private Mock<ILogger<ServiceBusManager>> _logger;
        private Mock<IServiceProvider> _serviceProvider;

        private const string TestHostName = "testhost";
        private const string TestServiceName = "testservicename";
        private const string ExchangeName = "micro-service-exchange";


        [SetUp]
        public void Setup()
        {
            _serviceConnection = new Mock<IServiceBusConnection>();
            _exchangeFactory = new Mock<IServiceBusExchangeFactory>();
            _queueFactory = new Mock<IServiceBusQueueFactory>();
            _subscriber = new Mock<IServiceBusMessageSubscriber>();
            _handlerFactory = new Mock<IHandlerFactory>();
            _settings = new Mock<IServiceSettings>();
            _serviceBusConnectionFactory = new Mock<IServiceBusConnectionFactory>();
            _settings.SetupAllProperties();
            _serviceSettings = new Mock<IServiceSettings>();
            _serviceProvider = new Mock<IServiceProvider>();
            _busSettings = new Mock<IServiceBusSettings>();
            _busSettings.SetupAllProperties();
            _serviceSettings.SetupAllProperties();
            _busSettings.Object.HostName = TestHostName;
            _serviceSettings.Object.Name = TestServiceName;
            _logger = new Mock<ILogger<ServiceBusManager>>();
            _queue = new Mock<IServiceBusQueue>();
            _queueFactory.Setup(o => o.CreateServiceBusQueue()).Returns(_queue.Object);
            _commandHandler = new Mock<MockCommandHandler>();
            _handlerFactory.Setup(o => o.ResolveCommandHandler<MockCommand>()).Returns(_commandHandler.Object);
            _exchange = new Mock<IServiceBusExchange>();
            _exchangeFactory.Setup(o => o.CreateServiceBusExchange()).Returns(_exchange.Object);
            _rabbitConnection = new Mock<IConnection>();
            _connectionFactory = new Mock<ConnectionFactory>();
            _connectionFactory.Setup(o => o.CreateConnection()).Returns(_rabbitConnection.Object);
            _serviceBusConnectionFactory.Setup(o => o.CreateConnectionFactory(It.IsAny<string>())).Returns(_connectionFactory.Object);
            _mockEventHandler = new Mock<MockEventHandler>();
            _handlerFactory.Setup(o => o.ResolveEventHandler<MockEvent>()).Returns(_mockEventHandler.Object);
        }

        [Test]
        public void CreateConnection_Always_CreatesServiceBusConnection()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);

            //Assert
            _connectionFactory.Verify(o => o.CreateConnection());
        }

        [Test]
        public void CreateConnection_Always_RegistersRabbitConnection()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);

            //Assert
            _serviceConnection.Verify(o => o.RegisterConnection(_rabbitConnection.Object));
        }

        [Test]
        public void CreateConnection_Always_CreatesAnExchange()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);

            //Assert
            _exchange.Verify(o => o.CreateExchange(ExchangeName, "topic"));
        }

        [Test]
        public void CreateConnection_Always_DeclaresQueue()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);

            //Assert
            _queue.Verify(o => o.DeclareQueue(_serviceSettings.Object.Name));
        }

        [Test]
        public void CreateConnection_Always_SetsServiceSettingsName()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);

            //Assert
            _settings.Object.Name.ShouldBe(TestServiceName);
        }


        [Test]
        public void SubscribeCommand_Always_SubscribesToAMessage()
        {
            //Arrange
            var sut = CreateSut();
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);

            //Act
            sut.SubscribeCommand<MockCommand>();

            //Assert
            _subscriber.Verify(o =>
                o.Subscribe<MockCommand>(TestServiceName, It.IsAny<Func<MockCommand, IRequestInfo, Task>>()));
        }

        [Test]
        public void SubscribeCommand_Always_CreatesAnExchangeBinding()
        {
            //Arrange   
            var sut = CreateSut();
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);
            _serviceSettings.Object.Name = TestServiceName;

            //Act
            sut.SubscribeCommand<MockCommand>();

            //Assert
            _queue.Verify(o => o.Bind(ExchangeName, $"test.MockCommand"));
        }

        [Test]
        public void SubscribeCommand_Always_CreatesCommandHandlerOfType()
        {
            //Arrange
            var sut = CreateSut();
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);

            //Act
            sut.SubscribeCommand<MockCommand>();

            //Assert
            _handlerFactory.Verify(o => o.ResolveCommandHandler<MockCommand>());
        }

        [Test]
        public void SubscribeEvent_Always_CreatesEventHandlerOfType()
        {
            //Arrange
            var sut = CreateSut();
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);

            //Act
            sut.SubscribeEvent<MockEvent>();

            //Assert
            _handlerFactory.Verify(o => o.ResolveEventHandler<MockEvent>());
        }

        [Test]
        public void SubscribeEvent_Always_SubscribesToAMessage()
        {
            //Arrange
            var sut = CreateSut();
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);

            //Act
            sut.SubscribeEvent<MockEvent>();

            //Assert
            _subscriber.Verify(o => o.Subscribe<MockEvent>(TestServiceName, It.IsAny<Func<MockEvent, IRequestInfo, Task>>()));
        }

        [Test]
        public void SubscribeEvent_Always_CreatesAnExchangeBinding()
        {
            //Arrange
            var sut = CreateSut();
            sut.CreateConnection(_busSettings.Object, _serviceSettings.Object);

            //Act
            sut.SubscribeEvent<MockEvent>();

            //Assert
            _queue.Verify(o => o.Bind(ExchangeName, "test.MockEvent"));
        }

        public ServiceBusManager CreateSut() => new ServiceBusManager(_serviceConnection.Object, _exchangeFactory.Object, _queueFactory.Object, _subscriber.Object, _settings.Object, _serviceBusConnectionFactory.Object, _handlerFactory.Object, _serviceProvider.Object, LoggerMock.Create<ServiceBusManager>());
    }
}
