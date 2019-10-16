using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using RabbitMQ.Client;
using Services.RabbitMq.Exchange;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Factories;

namespace Services.RabbitMq.Tests.Exchange
{
    public class ServiceBusExchangeTests
    {
        private Mock<IServiceBusConnectionFactory> _connectionFactory;
        private Mock<IServiceBusConnection> _connection;
        private Mock<IModel> _model;


        [SetUp]
        public void Setup()
        {
            _model = new Mock<IModel>();
            _connection = new Mock<IServiceBusConnection>();
            _connectionFactory = new Mock<IServiceBusConnectionFactory>();

            _connection.SetupGet(o => o.Channel).Returns(_model.Object);
            _connectionFactory.Setup(o => o.ResolveServiceBusConnection()).Returns(_connection.Object);
        }


        [Test]
        public void CreateExchange_Always_CreatesExchange()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            sut.CreateExchange("test", "test");

            //Assert
            _model.Verify(o => o.ExchangeDeclare("test", "test", false,false, null));
        }

        public ServiceBusExchange CreateSut() => new ServiceBusExchange(_connectionFactory.Object);
    }
}
