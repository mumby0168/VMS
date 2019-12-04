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
    public class UpdateHeadContactHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<IBusinessRepository> _repository;

        private Mock<IBusiness> _business;
        private Mock<IHeadContact> _contact;
        private Mock<UpdateHeadContact> _message;


        [SetUp]
        public void Setup()
        {   
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _repository = new Mock<IBusinessRepository>();

            _message = new Mock<UpdateHeadContact>();
            _business = new Mock<IBusiness>();
            _contact = new Mock<IHeadContact>();
            _business.Setup(o => o.GetContact()).Returns(_contact.Object);
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
            _contact.Verify(o => o.Update(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
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
            _publisher.Verify(o => o.PublishEvent(It.IsAny<BusinessContactUpdated>(), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesUpdateRejectedIfUpdateThrows()
        {
            //Arrange
            var sut = CreateSut();

            _repository.Setup(o => o.GetBusinessAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_business.Object));
            _contact.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new VmsException("", ""));

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<UpdateBusinessRejected>(), _requestInfo.Object));
        }

        public UpdateHeadContactHandler CreateSut() => new UpdateHeadContactHandler(LoggerMock.CreateVms<UpdateHeadContactHandler>(), _repository.Object, _publisher.Object);
    }
}
