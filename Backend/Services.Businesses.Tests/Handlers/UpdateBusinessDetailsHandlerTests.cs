using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services.Business.Domain;
using Services.Business.Handlers.Command;
using Services.Business.Messages.Commands;
using Services.Business.Messages.Events;
using Services.Business.Messages.Events.Rejected;
using Services.Business.Repositorys;
using Services.Common.Exceptions;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Tests.Mocks;

namespace Services.Businesses.Tests.Handlers
{
    public class UpdateBusinessDetailsHandlerTests
    {

        private Mock<IBusinessRepository> _repository;
        private Mock<IServiceBusMessagePublisher> _publisher;

        private Mock<IBusiness> _business;
        private Mock<UpdateBusinessDetails> _message;

        private Mock<IRequestInfo> _requestInfo;

        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _repository = new Mock<IBusinessRepository>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _business = new Mock<IBusiness>();
            _message = new Mock<UpdateBusinessDetails>();
        }

        [Test]
        public async Task HandleAsync_Always_PublishesUpdateRejectedIfNoBusinessWithIdExists()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<UpdateBusinessRejected>(), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_UpdatesBusinessIfExists()
        {
            //Arrange
            var sut = CreateSut();
            _repository.Setup(o => o.GetBusinessAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_business.Object));

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _business.Verify(o => o.Update(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public async Task HandleAsync_Always_UpdatesBusinessInRepoIfValid()
        {
            //Arrange
            var sut = CreateSut();
            _repository.Setup(o => o.GetBusinessAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_business.Object));

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _repository.Verify(o => o.UpdateAsync(_business.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishDetailsUpdatedIfValid()
        {
            //Arrange
            var sut = CreateSut();
            _repository.Setup(o => o.GetBusinessAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_business.Object));

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<BusinessDetailsUpdated>(), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesUpdateRejectedIfUpdateThrows()
        {
            //Arrange
            var sut = CreateSut();

            _repository.Setup(o => o.GetBusinessAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_business.Object));
            _business.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new VmsException("", ""));

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<UpdateBusinessRejected>(), _requestInfo.Object));
        }

        public UpdateBusinessDetailsHandler CreateSut() => new UpdateBusinessDetailsHandler(LoggerMock.CreateVms<UpdateBusinessDetailsHandler>(), _repository.Object, _publisher.Object);
    }
}
