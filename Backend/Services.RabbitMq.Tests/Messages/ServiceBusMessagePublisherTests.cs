using Moq;
using NUnit.Framework;
using RabbitMQ.Client;
using Services.RabbitMq.Interfaces;
using Services.RabbitMq.Interfaces.Factories;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Interfaces.Settings;
using Services.RabbitMq.Interfaces.Wrappers;
using Services.RabbitMq.Messages;
using Services.Tests.Mocks.Messaging;


namespace Services.RabbitMq.Tests.Messages
{
    public class ServiceBusMessagePublisherTests
    {

        private Mock<IServiceBusConnectionFactory> _connectionFactory;
        private Mock<IServiceBusConnection> _connection;
        private Mock<IModel> _model;
        private Mock<IJsonConvertWrapper> _jsonWrapper;
        private Mock<IUtf8Wrapper> _utf8Wrapper;
        private Mock<IServiceSettings> _serviceSettings;
        private Mock<MockCommand> _mockCommand;
        private const string TestDefaultServiceName = "test-service";
        private Mock<MockEvent> _event;
        private Mock<IRequestInfo> _requestInfo;

        [SetUp]
        public void Setup()
        {
            _model = new Mock<IModel>();
            _connection = new Mock<IServiceBusConnection>();
            _connectionFactory = new Mock<IServiceBusConnectionFactory>();
            _jsonWrapper = new Mock<IJsonConvertWrapper>();
            _utf8Wrapper = new Mock<IUtf8Wrapper>();
            _serviceSettings = new Mock<IServiceSettings>();
            _mockCommand = new Mock<MockCommand>();
            _serviceSettings.SetupGet(o => o.Name).Returns("test-service");
            _connection.SetupGet(o => o.Channel).Returns(_model.Object);
            _connectionFactory.Setup(o => o.ResolveServiceBusConnection()).Returns(_connection.Object);
            _event = new Mock<MockEvent>();
            _requestInfo = new Mock<IRequestInfo>();
        }

        [TestCase]
        public void PublishCommand_Always_PublishesWithCorrectRoutingKey()
        {   
            //Arrange
            var sut = CreateSut();

            //Act   
            sut.PublishCommand(_mockCommand.Object, _requestInfo.Object);

            //Assert
            _model.Verify(o => o.BasicPublish(It.IsAny<string>(), "test-service.MockCommand", true, It.IsAny<IBasicProperties>(), It.IsAny<byte[]>()));

        }

        [Test]
        public void PublishCommand_Always_PublishesWithCorrectMessageBody()
        {
            //Arrange
            var sut = CreateSut();
            var bytes = new byte[] {10, 20, 30, 40};
            _utf8Wrapper.Setup(o => o.GetBytes(It.IsAny<string>())).Returns(bytes);


            //Act
            sut.PublishCommand(_mockCommand.Object, _requestInfo.Object);

            //Assert
            _model.Verify(o => o.BasicPublish("micro-service-exchange", "test-service.MockCommand", true, It.IsAny<IBasicProperties>(), bytes));
        }

        [Test]
        public void PublishEvent_Always_PublishesWithCorrectRoutingKey()
        {
            //Arrange
            var sut = CreateSut();

            //Act   
            sut.PublishEvent(_event.Object, _requestInfo.Object);

            //Assert
            _model.Verify(o => o.BasicPublish(It.IsAny<string>(), "test-service.MockEvent", true, It.IsAny<IBasicProperties>(), It.IsAny<byte[]>()));
        }

        [Test]
        public void PublishEvent_Always_PublishesWithCorrectMessageBody()
        {
            //Arrange
            var sut = CreateSut();
            var bytes = new byte[] {10, 20, 30, 40};
            _utf8Wrapper.Setup(o => o.GetBytes(It.IsAny<string>())).Returns(bytes);

            //Act
            sut.PublishEvent(_event.Object, _requestInfo.Object);

            //Assert
            _model.Verify(o => o.BasicPublish("micro-service-exchange", "test-service.MockEvent", true,
                It.IsAny<IBasicProperties>(), bytes));
        }

        public ServiceBusMessagePublisher CreateSut() => new ServiceBusMessagePublisher(_connectionFactory.Object, _serviceSettings.Object, _jsonWrapper.Object, _utf8Wrapper.Object);
    }
}
