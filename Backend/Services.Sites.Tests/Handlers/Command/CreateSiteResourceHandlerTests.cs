using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services.Common.Exceptions;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Sites.Domain;
using Services.Sites.Factories;
using Services.Sites.Handlers.Command;
using Services.Sites.Messages.Commands;
using Services.Sites.Messages.Events.Send;
using Services.Sites.Messages.Events.Send.Rejected;
using Services.Sites.Repositorys;
using Services.Tests.Mocks;

namespace Services.Sites.Tests.Handlers.Command
{
    public class CreateSiteResourceHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<ISiteServiceFactory> _factory;
        private Mock<ISiteResourceRepository> _repository;
        private Mock<ISiteRepository> _siteRepository;

        private Mock<ISiteResource> _resource;
        private Mock<CreateSiteResource> _message;


        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _factory = new Mock<ISiteServiceFactory>();
            _repository = new Mock<ISiteResourceRepository>();
            _siteRepository = new Mock<ISiteRepository>();

            _resource = new Mock<ISiteResource>();
            _message = new Mock<CreateSiteResource>();

            _siteRepository.Setup(o => o.IsSiteIdValid(It.IsAny<Guid>())).Returns(Task.FromResult(true));
            _factory.Setup(o => o.CreateSiteResource(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_resource.Object);
        }

        [Test]
        public async Task HandleAsync_Always_RejectsIfSiteIsNotFoundFromId()
        {
            //Arrange
            var sut = CreateSut();
            _siteRepository.Setup(o => o.IsSiteIdValid(It.IsAny<Guid>())).Returns(Task.FromResult(false));

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<SiteResourceRejected>(r => r.Code == Codes.InvalidSiteId), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_RejectsIfDomainValidationFails()
        {
            //Arrange
            var sut = CreateSut();

            string code = "code";
            string reason = "reason";
            _factory.Setup(o => o.CreateSiteResource(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new VmsException(code, reason));

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<SiteResourceRejected>(r => r.Code == code && r.Reason == reason), _requestInfo.Object));
        }


        [Test]
        public async Task HandleAsync_Always_AddToDbIfValid()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _repository.Verify(o => o.AddAsync(_resource.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesCreatedIfResourceAddedToDb()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<SiteResourceCreated>(), _requestInfo.Object));
        }

        public CreateSiteResourceHandler CreateSut() => new CreateSiteResourceHandler(LoggerMock.Create<CreateSiteResourceHandler>(), _publisher.Object, _factory.Object, _repository.Object, _siteRepository.Object);
    }
}
