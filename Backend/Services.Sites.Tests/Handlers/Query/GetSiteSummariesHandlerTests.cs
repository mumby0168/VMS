using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetSiteSummariesHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<ISiteRepository> _repository;


        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _repository = new Mock<ISiteRepository>();
        }

        [Test]
        public async Task HandleAsync_Always_ReturnsDtoCollectionIfObjectsInDb()
        {
            //Arrange
            var sut = CreateSut();

            _repository.Setup(o => o.GetSitesForBusinessAsync(It.IsAny<Guid>())).Returns(
                Task.FromResult(new List<ISite> {new Mock<ISite>().Object, new Mock<ISite>().Object , new Mock<ISite>().Object }.AsEnumerable()));

            //Act
            var res = await sut.HandleAsync(new GetSiteSummaries(Guid.NewGuid()));

            //Assert
            res.Count().ShouldBe(3);
        }

        [Test]
        public async Task HandleAsync_Always_ReturnsNullIfNotInDb()
        {
            //Arrange
            var sut = CreateSut();


            //Act
            var res = await sut.HandleAsync(new GetSiteSummaries(Guid.NewGuid()));

            //Assert
            res.ShouldBeEmpty();
        }

        public GetSiteSummariesHandler CreateSut() => new GetSiteSummariesHandler(LoggerMock.CreateVms<GetSiteSummariesHandler>(), _repository.Object);
    }
}
