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
    public class CreateSiteHandlerTests
    {
        private Mock<ISiteServiceFactory> _factory;
        private Mock<ISiteRepository> _repository;
        private Mock<IBusinessRepository> _businessRepository;

        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<IRequestInfo> _requestInfo;

        private Mock<ISite> _site;
        private Mock<IContact> _contact;
        private Mock<CreateSite> _createSite;



        [SetUp]
        public void Setup()
        {
            _factory = new Mock<ISiteServiceFactory>();
            _repository = new Mock<ISiteRepository>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _businessRepository = new Mock<IBusinessRepository>();
            _site = new Mock<ISite>();
            _contact = new Mock<IContact>();
            _createSite = new Mock<CreateSite>();
            _requestInfo = new Mock<IRequestInfo>();

            _businessRepository.Setup(o => o.IsBusinessValidAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            _factory.Setup(o => o.CreateSite(It.IsAny<Guid>(),It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<IContact>())).Returns(_site.Object);

            _factory.Setup(o =>
                o.CreateContact(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_contact.Object);

        }

        [Test]
        public async Task HandleAsync_Always_AddSiteToDataStoreIfValid()
        {   
            //Arrange
            var sut = CreateSut();
                
            //Act
            await sut.HandleAsync(_createSite.Object, _requestInfo.Object);

            //Assert
            _repository.Verify(o => o.AddAsync(_site.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesSiteCreatedIfValid()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(_createSite.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<SiteCreated>(), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesRejectedIfSiteDomainValidationFails()
        {
            //Arrange
            var sut = CreateSut();
            _factory.Setup(o => o.CreateSite(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<IContact>()))
                .Throws(new VmsException(It.IsAny<string>(), It.IsAny<string>()));
            
            //Act
            await sut.HandleAsync(_createSite.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<CreateSiteRejected>(), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesRejectedIfContactDomainValidationFails()
        {
            //Arrange
            var sut = CreateSut();


            _factory.Setup(o =>
                    o.CreateContact(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new VmsException(It.IsAny<string>(), It.IsAny<string>()));


            //Act
            await sut.HandleAsync(_createSite.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<CreateSiteRejected>(), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesRejectedEventIfBusinessIdCannotBeFound()
        {
            //Arrange
            var sut = CreateSut();
            _businessRepository.Setup(o => o.IsBusinessValidAsync(It.IsAny<Guid>())).Returns(Task.FromResult(false));

            //Act
            await sut.HandleAsync(_createSite.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<CreateSiteRejected>(), _requestInfo.Object));
        }



        public CreateSiteHandler CreateSut() => new CreateSiteHandler(LoggerMock.Create<CreateSiteHandler>(), _factory.Object, _repository.Object,_publisher.Object, _businessRepository.Object);

    }
}
