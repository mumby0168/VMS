using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Sites.Domain;
using Services.Sites.Handlers.Query;
using Services.Sites.Messages.Queries;
using Services.Sites.Repositorys;
using Services.Tests.Mocks;
using Shouldly;

namespace Services.Sites.Tests.Handlers.Query
{
    class GetSiteHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<ISiteRepository> _repository;
        private Mock<Domain.ISiteDocument> _site;
        private Mock<IContact> _contact;


        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _repository = new Mock<ISiteRepository>();
            _site = new Mock<Domain.ISiteDocument>();
            _contact = new Mock<IContact>();
            _site.Setup(o => o.GetContact()).Returns(_contact.Object);
        }

        [Test]  
        public async Task HandleAsync_Always_ReturnsDtoIfObjectsInDb()
        {
            //Arrange
            var sut = CreateSut();

            _repository.Setup(o => o.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_site.Object));

            //Act
            var res = await sut.HandleAsync(new GetSite(Guid.NewGuid()));

            //Assert
            res.ShouldNotBeNull();
        }

        [Test]
        public async Task HandleAsync_Always_ReturnsNullIfNotInDb()
        {
            //Arrange
            var sut = CreateSut();


            //Act
            var res = await sut.HandleAsync(new GetSite(Guid.NewGuid()));

            //Assert
            res.ShouldBeNull();
        }

        public GetSiteHandler CreateSut() => new GetSiteHandler(LoggerMock.CreateVms<GetSiteHandler>(), _repository.Object);
    }
}
