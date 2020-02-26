using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services.Common.Exceptions;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Tests.Mocks;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Events;
using Services.Visitors.Factories;
using Services.Visitors.Handlers.Command;
using Services.Visitors.Names;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Tests
{
    public class CreateDataEntryHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<IDataSpecificationFactory> _factory;
        private Mock<IDataSpecificationRepository> _repository;


        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _factory = new Mock<IDataSpecificationFactory>();
            _repository = new Mock<IDataSpecificationRepository>();

            _factory.Setup(o => o.Create(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<Guid>())).Returns(new Mock<IDataSpecificationDocument>().Object);
        }

        [Test]
        public async Task HandleAsync_Always_GetsNextOrderNumber()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(new CreateDataEntry("Test", "message", "Required",
                Guid.NewGuid()), _requestInfo.Object);

            //Assert
            _repository.Verify(o => o.GetNextOrderNumberAsync(It.IsAny<Guid>()));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesDataSpecificationRejectedWhenValidationFails()
        {
            //Arrange
            var sut = CreateSut();
            _factory.Setup(o => o.Create(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<Guid>())).Throws(new VmsException("code", "reason"));

            //Act
            await sut.HandleAsync(new CreateDataEntry("Test", "message", "Required",
                Guid.NewGuid()), _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<DataSpecificationRejected>(x => x.Code == "code" && x.Reason == "reason"), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_CreatesSpecificationIfValid()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(new CreateDataEntry("Test", "message", "Required",
                Guid.NewGuid()), _requestInfo.Object);


            //Assert
            _repository.Verify(o => o.AddAsync(It.IsAny<IDataSpecificationDocument>()));
            _publisher.Verify(o => o.PublishEvent(It.IsAny<DataSpecificationCreated>(), _requestInfo.Object));
        }

        public CreateDataEntryHandler CreateSut() => new CreateDataEntryHandler(LoggerMock.CreateVms<CreateDataEntryHandler>(), _publisher.Object, _factory.Object,
            _repository.Object);
    }
}