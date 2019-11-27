using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetSiteResourcesHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<ISiteResourceRepository> _repository;
        private readonly Guid _id = Guid.NewGuid();
        


        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _repository = new Mock<ISiteResourceRepository>();
        }

        [TestCase(3)]
        [TestCase(1)]
        [TestCase(10)]
        public async Task HandleAsync_Always_ReturnsAmountOfDtosAsDomianObjects(int items)
        {
            //Arrange
            var sut = CreateSut();
            var list = new List<ISiteResource>();
            for (int i = 0; i < items; i++) list.Add(new Mock<ISiteResource>().Object);
            _repository.Setup(o => o.GetSiteResources(It.IsAny<Guid>())).Returns(Task.FromResult(list.AsEnumerable()));

            //Act
            var res = await sut.HandleAsync(new GetSiteResources(Guid.Empty));

            //Assert
            res.Count().ShouldBe(items);
        }

        [Test]
        public async Task HandleAsync_Always_ReturnsEmptyListIfNoData()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var res = await sut.HandleAsync(new GetSiteResources(Guid.Empty));

            //Assert
            res.ShouldBeEmpty();
        }



        public GetSiteResourcesHandler CreateSut() => new GetSiteResourcesHandler(LoggerMock.Create<GetSiteResourcesHandler>(), _repository.Object);
    }
}
