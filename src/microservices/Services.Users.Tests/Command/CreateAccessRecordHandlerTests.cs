using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services.Common.Mongo;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Tests.Mocks;
using Services.Users.Commands;
using Services.Users.Domain;
using Services.Users.Events;
using Services.Users.Factories;
using Services.Users.Handlers.Command;
using Services.Users.Repositories;
using Services.Users.Services;

namespace Services.Users.Tests.Command
{

    public class CreateAccessRecordHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<IUserRepository> _userRepo;
        private Mock<IAccessRecordRepository> _accessRecordRepo;
        private Mock<IAccessRecordFactory> _accessFactory;
        private Mock<IUserStatusService> _userStatusService;
        private Mock<IServicesRepository> _serviceRepository;

        private Mock<IUserDocument> _user;
        private Mock<IAccessRecordDocument> _record; 


        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _accessFactory = new Mock<IAccessRecordFactory>();
            _accessRecordRepo = new Mock<IAccessRecordRepository>();
            _userRepo = new Mock<IUserRepository>();
            _user = new Mock<IUserDocument>();
            _record = new Mock<IAccessRecordDocument>();
            _userStatusService = new Mock<IUserStatusService>();
            _serviceRepository = new Mock<IServicesRepository>();
        }

        [TestCase(AccessAction.In)]
        [TestCase(AccessAction.Out)]
        public async Task HandleAsync_Always_PublishesAccessRejectedIfUsersCannotBeFound(AccessAction action)
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(new CreateAccessRecord(It.IsAny<int>(), action, It.IsAny<Guid>()), _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<AccessRecordRejected>(), _requestInfo.Object));
        }


        [TestCase(AccessAction.In)]
        [TestCase(AccessAction.Out)]
        public async Task HandleAsync_Always_CreatesRecordIfUserValid(AccessAction action)
        {
            //Arrange
            var sut = CreateSut();
            _userRepo.Setup(o => o.GetByCodeAsync(It.IsAny<int>())).Returns(Task.FromResult(_user.Object));
            _accessFactory.Setup(o => 
                    o.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), action, It.IsAny<Guid>()))
                .Returns(_record.Object);

            _serviceRepository.Setup(o => o.IsSiteIdValid(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            //Act
            await sut.HandleAsync(new CreateAccessRecord(It.IsAny<int>(), action, It.IsAny<Guid>()), _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<AccessRecordCreated>(), _requestInfo.Object));
            _accessRecordRepo.Verify(o => o.AddAsync(_record.Object));
        }



        public CreateAccessRecordHandler CreateSut() => new CreateAccessRecordHandler(LoggerMock.CreateVms<CreateAccessRecordHandler>(), _userRepo.Object, _accessRecordRepo.Object, _accessFactory.Object, _publisher.Object
         , _serviceRepository.Object  , _userStatusService.Object);
        
    }
}
