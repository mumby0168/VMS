using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using RabbitMQ.Client;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Exchange;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Queue;

namespace Services.RabbitMq.Tests.Queue
{
    public class ServiceBusQueueTests
    {
        private Mock<IServiceBusConnectionFactory> _connectionFactory;
        private Mock<IServiceBusConnection> _connection;
        private Mock<IServiceBusExchangeFactory> _exchangeFactory;
        private Mock<IModel> _model;
        private Mock<IServiceBusExchange> _exchange;


        [SetUp]
        public void Setup()
        {
            _model = new Mock<IModel>();
            _connection = new Mock<IServiceBusConnection>();
            _connectionFactory = new Mock<IServiceBusConnectionFactory>();
            _exchangeFactory = new Mock<IServiceBusExchangeFactory>();
            _exchange = new Mock<IServiceBusExchange>();
            _exchangeFactory.Setup(o => o.CreateServiceBusExchange()).Returns(_exchange.Object);
            _connection.SetupGet(o => o.Channel).Returns(_model.Object);
            _connectionFactory.Setup(o => o.ResolveServiceBusConnection()).Returns(_connection.Object);

        }

        [Test]
        public void DeclareQueue_Always_DeclaresQueueWithName()
        {
            //Arrange   
            var sut = CreateSut();

            //Act
            sut.DeclareQueue("test-queue");

            //Assert
            _model.Verify(o => o.QueueDeclare("test-queue", false, false, true, null));
        }

        [Test]
        public void Bind_Always_BindsToQueueWithRoutingKey()
        {
            //Arrange
            var sut = CreateSut();
            sut.DeclareQueue("test-queue");

            //Act
            sut.Bind("name", "key");

            //Assert
            _model.Verify(o => o.QueueBind("test-queue", "name", "key", null));
        }

        public ServiceBusQueue CreateSut() => new ServiceBusQueue(_connectionFactory.Object, _exchangeFactory.Object);
    }
}
