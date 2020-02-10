using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Sites.Domain;
using Services.Sites.Handlers.Events;
using Services.Sites.Messages.Events;
using Services.Sites.Repositorys;
using Services.Tests.Mocks;

namespace Services.Sites.Tests.Handlers.Event
{
    class BusinessCreatedHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<IBusinessRepository> _repository;
        private Mock<BusinessCreated> _message;


        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _repository = new Mock<IBusinessRepository>();
            _message = new Mock<BusinessCreated>();
        }

        [Test]
        public async Task HandleAsync_Always_CreatesBusiness()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _repository.Verify(o => o.AddAsync(It.IsAny<IBusiness>()));
        }

        public BusinessCreatedHandler CreateSut() => new BusinessCreatedHandler(LoggerMock.CreateVms<BusinessCreatedHandler>(), _repository.Object);   
    }
}
