using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Sites.Handlers.Command;
using Services.Sites.Messages.Commands;
using Services.Sites.Repositorys;
using Services.Tests.Mocks;

namespace Services.Sites.Tests.Handlers.Command
{
    public class RemoveSiteResourceHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<ISiteResourceRepository> _repository;

        private Mock<RemoveSiteResource> _message;


        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _repository = new Mock<ISiteResourceRepository>();
            _message = new Mock<RemoveSiteResource>();
        }

        [Test]
        public async Task HandleAsync_Always_RemovesFromDb()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(_message.Object, _requestInfo.Object);

            //Assert
            _repository.Verify(o => o.RemoveAsync(It.IsAny<Guid>()));
        }

        public RemoveSiteResourceHandler CreateSut() => new RemoveSiteResourceHandler(LoggerMock.CreateVms<RemoveSiteResourceHandler>(), _publisher.Object, _repository.Object);
    }
}
