using System;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Visitors.Commands;
using Services.Visitors.Dtos;
using Services.Visitors.Events;
using Services.Visitors.Handlers.Command;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Tests
{
    
    public class CreateVisitorHandlerTests
    {

        private AutoMocker _mocker;
        private Mock<IUserServiceClient> _userClient;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<ISiteServiceClient> _siteClient;
        private Guid _validUserGuid = Guid.NewGuid();
        private Guid _validSiteGuid = Guid.NewGuid();
        private Mock<SiteDto> _siteDto;

        [SetUp]
        public void SetUp()
        {
            
            _siteDto = new Mock<SiteDto>();
            
            _siteClient = new Mock<ISiteServiceClient>();
            _userClient = new Mock<IUserServiceClient>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _mocker = new AutoMocker();
            _mocker.Use(_publisher.Object);
            _mocker.Use(_userClient.Object);
            _mocker.Use(_siteClient.Object);

            _userClient.Setup(o => o.ContainsUserAsync(_validUserGuid)).Returns(Task.FromResult(true));
            _siteClient.Setup(o => o.GetSiteAsync(_validUserGuid)).Returns(Task.FromResult(_siteDto.Object));
        }

        [TestCase]
        public async Task HandleAsync_Always_ThrowsIfUserIdNotFound()
        {
            //Arrange
            var sut = CreateSut();

            _userClient.Setup(o => o.ContainsUserAsync(It.IsAny<Guid>())).Returns(Task.FromResult(false));

            //Act
            await  sut.HandleAsync(new CreateVisitor(Guid.Empty, Guid.Empty, null), new Mock<IRequestInfo>().Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<CreateVisitorRejected>(), It.IsAny<IRequestInfo>()));
        }
        
        
        [TestCase]
        public async Task HandleAsync_Always_ThrowsIfSiteNotFound()
        {
            //Arrange
            var sut = CreateSut();

            _siteClient.Setup(o => o.GetSiteAsync(It.IsAny<Guid>())).Returns(Task.FromResult((SiteDto) null));

            //Act
            await  sut.HandleAsync(new CreateVisitor(Guid.Empty, _validUserGuid, null), new Mock<IRequestInfo>().Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<CreateVisitorRejected>(), It.IsAny<IRequestInfo>()));
        }
        

        public CreateVisitorHandler CreateSut() => _mocker.CreateInstance<CreateVisitorHandler>();
    }
}