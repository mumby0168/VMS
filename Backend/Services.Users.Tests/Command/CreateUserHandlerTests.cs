using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Moq;
using NUnit.Framework;
using Services.Common.Exceptions;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Tests.Mocks;
using Services.Users.Commands;
using Services.Users.Domain;
using Services.Users.Events;
using Services.Users.Factories;
using Services.Users.Handlers.Command;
using Services.Users.Handlers.Events;
using Services.Users.Repositories;
using Services.Users.Services;

namespace Services.Users.Tests.Command
{
    public class CreateUserHandlerTests
    {

        private Mock<IRequestInfo> _requestInfo;
        private Mock<IServiceBusMessagePublisher> _publisher;
        private Mock<IUsersFactory> _userFactory;
        private Mock<IAccountRepository> _accountRepo;
        private Mock<IServicesRepository> _serviceRepo;
        private Mock<IUserRepository> _usersRepo;

        private CreateUser _createUser = new CreateUser("Fred", "Ball", "07568 675445", "01482 657434", Guid.NewGuid(),
            Guid.NewGuid(), Guid.NewGuid());

        private Mock<IUser> _user;


        [SetUp]
        public void Setup()
        {
            _requestInfo = new Mock<IRequestInfo>();
            _publisher = new Mock<IServiceBusMessagePublisher>();
            _userFactory = new Mock<IUsersFactory>();
            _accountRepo = new Mock<IAccountRepository>();
            _serviceRepo = new Mock<IServicesRepository>();
            _usersRepo = new Mock<IUserRepository>();
            _user = new Mock<IUser>();
        }

        [Test]
        public async Task HandleAsync_Always_SendsUserRejectedEventWhenAccountDoesNotExist()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.HandleAsync(_createUser, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<CreateUserRejected>(x => x.Code == Codes.InvalidId && x.Reason.Contains("account")), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_SendsUserRejectedEventWhenBusinessDoesNotExist()
        {
            //Arrange
            var sut = CreateSut();
            _accountRepo.Setup(o => o.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Mock<IAccount>().Object));

            //Act
            await sut.HandleAsync(_createUser, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<CreateUserRejected>(x => x.Code == Codes.InvalidId && x.Reason.Contains("business")), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_SendsUserRejectedEventWhenSiteDoesNotExist()
        {
            //Arrange
            var sut = CreateSut();
            _accountRepo.Setup(o => o.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Mock<IAccount>().Object));
            _serviceRepo.Setup(o => o.IsBusinessIdValid(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            //Act
            await sut.HandleAsync(_createUser, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<CreateUserRejected>(x => x.Code == Codes.InvalidId && x.Reason.Contains("site")), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_SendsUserRejectedEventWhenDomainValidationFails()
        {   
            //Arrange
            var sut = CreateSut();
            _accountRepo.Setup(o => o.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Mock<IAccount>().Object));
            _serviceRepo.Setup(o => o.IsBusinessIdValid(It.IsAny<Guid>())).Returns(Task.FromResult(true));
            _serviceRepo.Setup(o => o.IsSiteIdValid(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            _userFactory
                .Setup(o => o.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws(new VmsException("code", "reason"));

            //Act
            await sut.HandleAsync(_createUser, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.Is<CreateUserRejected>(x => x.Code == "code" && x.Reason == "reason"), _requestInfo.Object));
        }

        [Test]
        public async Task HandleAsync_Always_CreatesUserWhenValid() 
        {
            //Arrange
            var sut = CreateSut();
            _accountRepo.Setup(o => o.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Mock<IAccount>().Object));
            _serviceRepo.Setup(o => o.IsBusinessIdValid(It.IsAny<Guid>())).Returns(Task.FromResult(true));
            _serviceRepo.Setup(o => o.IsSiteIdValid(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            _userFactory
                .Setup(o => o.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(_user.Object);

            //Act
            await sut.HandleAsync(_createUser, _requestInfo.Object);

            //Assert
            _publisher.Verify(o => o.PublishEvent(It.IsAny<UserCreated>(), _requestInfo.Object));
            _usersRepo.Verify(o => o.AddAsync(_user.Object));
        }

        public CreateUserHandler CreateSut() => new CreateUserHandler(LoggerMock.CreateVms<CreateUserHandler>(), _userFactory.Object, _accountRepo.Object, _usersRepo.Object, _publisher.Object, _serviceRepo.Object);
    }
}
