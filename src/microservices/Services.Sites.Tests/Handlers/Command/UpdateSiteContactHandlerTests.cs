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
using Services.Sites.Handlers.Command;
using Services.Sites.Messages.Commands;
using Services.Sites.Messages.Events.Send;
using Services.Sites.Messages.Events.Send.Rejected;
using Services.Sites.Repositorys;
using Services.Tests.Mocks;

namespace Services.Sites.Tests.Handlers.Command
{
    public class UpdateSiteContactHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<ISite> _site;
        private Mock<IContact> _contact;
        private Mock<UpdateSiteContact> _message;

        [SetUp]
        public void Setup()
        {
            _site = new Mock<ISite>();
            _contact = new Mock<IContact>();
            _message = new Mock<UpdateSiteContact>();
            _requestInfo = new Mock<IRequestInfo>();
            _siteRepository = new Mock<ISiteRepository>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _site.Setup(o => o.GetContact()).Returns(_contact.Object);
        }

        [Test]  
        public async Task HandleAsync_Always_PublishesSiteUpdatedWhenContactDetailsUpdated()
        {
            //Arrange
            var sut = CreateSut();

            _siteRepository.Setup(o => o.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_site.Object));

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<SiteUpdated>(), _requestInfo.Object));
        }


        [Test]
        public async Task HandleAsync_Always_UpdatesDomainObjectWhenDetailsUpdateSuccesful()
        {
            //Arrange
            var sut = CreateSut();

            _siteRepository.Setup(o => o.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_site.Object));

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _siteRepository.Verify(o => o.Update(_site.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesRejectedEventWhenSiteIsNotInDb()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<SiteUpdateRejected>(r => r.Code == Codes.InvalidId), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_PublishesRejectedEventWhenUpdateFailsValidation()
        {
            //Arrange
            var sut = CreateSut();
            string code = "code";
            string reason = "reason";

            _siteRepository.Setup(o => o.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_site.Object));
            _contact.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new VmsException(code, reason));

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<SiteUpdateRejected>(r => r.Code == code && r.Reason == reason), _requestInfo.Object));
        }

        public UpdateSiteContactHandler CreateSut() => new UpdateSiteContactHandler(LoggerMock.CreateVms<UpdateSiteContactHandler>(), _publisher.Object, _siteRepository.Object);
    }
}
