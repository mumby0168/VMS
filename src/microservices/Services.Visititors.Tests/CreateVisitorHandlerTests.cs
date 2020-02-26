using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using Services.Common.Exceptions;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Visitors.Commands;
using Services.Visitors.Domain;
using Services.Visitors.Dtos;
using Services.Visitors.Events;
using Services.Visitors.Factories;
using Services.Visitors.Handlers.Command;
using Services.Visitors.Repositorys;
using Services.Visitors.Services;

namespace Services.Visitors.Tests
{
    
    public class CreateVisitorHandlerTests
    {

        private AutoMocker _mocker;
        private Mock<IUserServiceClient> _userClient;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<ISiteServiceClient> _siteClient;
        private Mock<IVisitorFormValidatorService> _validationService;
        private Mock<IVisitorsRepository> _repo;
        private Mock<IVisitorFactory> _factory;
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
            _factory = new Mock<IVisitorFactory>();
            _repo = new Mock<IVisitorsRepository>();
            _validationService = new Mock<IVisitorFormValidatorService>();
            _mocker = new AutoMocker();
            _mocker.Use(_publisher.Object);
            _mocker.Use(_userClient.Object);
            _mocker.Use(_siteClient.Object);
            _mocker.Use(_validationService.Object);
            _mocker.Use(_repo);
            _mocker.Use(_factory);

            _factory.Setup(o => o.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(),
                It.IsAny<IEnumerable<VisitorData>>())).Returns(new Mock<IVisitorDocument>().Object);

            _userClient.Setup(o => o.ContainsUserAsync(_validUserGuid)).Returns(Task.FromResult(true));
            _siteClient.Setup(o => o.GetSiteAsync(_validSiteGuid)).Returns(Task.FromResult(_siteDto.Object));
        }

        [TestCase]
        public async Task HandleAsync_Always_RejectsIfUserIdNotFound()
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
        public async Task HandleAsync_Always_RejectsIfSiteNotFound()
        {
            //Arrange
            var sut = CreateSut();

            _siteClient.Setup(o => o.GetSiteAsync(It.IsAny<Guid>())).Returns(Task.FromResult((SiteDto) null));

            //Act
            await  sut.HandleAsync(new CreateVisitor(Guid.Empty, _validUserGuid, null), new Mock<IRequestInfo>().Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<CreateVisitorRejected>(), It.IsAny<IRequestInfo>()));
        }
        
        [TestCase]
        public async Task HandleAsync_Always_RejectsIfDataIsInvalid()
        {
            //Arrange
            var sut = CreateSut();

            _validationService
                .Setup(o => o.Validate(_siteDto.Object.BusinessId, It.IsAny<IEnumerable<VisitorDataEntry>>()))
                .Throws(new VmsException("code", "message"));

            //Act
            await  sut.HandleAsync(new CreateVisitor(_validSiteGuid, _validUserGuid, null), new Mock<IRequestInfo>().Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<CreateVisitorRejected>(m => m.Code == "code" && m.Reason == "message"), It.IsAny<IRequestInfo>()));
        }

        [TestCase]
        public async Task HandleAsync_Always_SavesIfDataValid()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await  sut.HandleAsync(new CreateVisitor(_validSiteGuid, _validUserGuid, new List<VisitorDataEntry>()), new Mock<IRequestInfo>().Object);

            //Assert
            _repo.Verify(o => o.AddAsync(It.IsAny<IVisitorDocument>()));
            _publisher.Verify(o => o.PublishEvent(It.IsAny<VisitorCreated>(), It.IsAny<IRequestInfo>()));
        }
        
        
        

        private CreateVisitorHandler CreateSut() => _mocker.CreateInstance<CreateVisitorHandler>();
    }
}