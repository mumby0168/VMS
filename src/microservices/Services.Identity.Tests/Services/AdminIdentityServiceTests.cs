﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Services.Common.Exceptions;
using Services.Common.Jwt;
using Services.Identity.Domain;
using Services.Identity.Managers;
using Services.Identity.Messages.Events;
using Services.Identity.Models;
using Services.Identity.Repositorys;
using Services.Identity.Services;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using Services.Tests.Mocks;
using Shouldly;

namespace Services.Identity.Tests.Managers
{
    public class AdminIdentityServiceTests
    {
        private Mock<IIdentityRepository> _identityRepo;
        private Mock<IJwtManager> _jwtManager;
        private Mock<IPendingIdentityRepository> _pendingRepo;
        private Mock<IServiceBusMessagePublisher> _serviceBusMessagePublisher;
        private Mock<IPasswordManager> _passwordManager;
        private Mock<ILogger<AdminIdentityService>> _logger;
        private Mock<IRefreshTokenService> _refreshTokenService;
        private Mock<IBusinessRepository> _businessRepository;

        private Mock<Domain.Identity> _identity;
        private Mock<PendingIdentity> _pendingIdentity;
        private Mock<IPassword> _password;



        private const string TestEmail = "test@test.com";
        private const string TestInUseEmail = "test1@test.com";
        private const string TestInValidEmail = "test12@test.com";
        private const string TestPassword = "Test123";
        private readonly Guid _invalidCode = Guid.NewGuid();
        private readonly Guid _validCode = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _identityRepo = new Mock<IIdentityRepository>();
            _jwtManager = new Mock<IJwtManager>();
            _passwordManager = new Mock<IPasswordManager>();
            _pendingRepo = new Mock<IPendingIdentityRepository>();
            _serviceBusMessagePublisher = new Mock<IServiceBusMessagePublisher>();
            _refreshTokenService = new Mock<IRefreshTokenService>();
            _logger = new Mock<ILogger<AdminIdentityService>>();
            _businessRepository = new Mock<IBusinessRepository>();

            _identity = new Mock<Domain.Identity>();
            _pendingIdentity = new Mock<PendingIdentity>();
            _password = new Mock<IPassword>();
            _password.SetupAllProperties();

            _businessRepository.Setup(o => o.ContainsBusinessAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));
            _identityRepo.Setup(o => o.IsEmailInUse(TestInUseEmail, It.IsAny<string>())).Returns(Task.FromResult(true));
            _identityRepo.Setup(o => o.IsEmailInUse(TestEmail, It.IsAny<string>())).Returns(Task.FromResult(false));
            _pendingRepo.Setup(o => o.IsEmailInUse(TestInUseEmail, It.IsAny<string>())).Returns(Task.FromResult(true));
            _pendingRepo.Setup(o => o.IsEmailInUse(TestEmail, It.IsAny<string>())).Returns(Task.FromResult(false));
            _pendingRepo.Setup(o => o.GetAsync(_validCode, TestEmail)).Returns(Task.FromResult(_pendingIdentity.Object));
            _passwordManager.Setup(o => o.EncryptPassword(TestPassword)).Returns(_password.Object);
        }


        [Test]
        public void SignIn_Always_ThrowsWhenNoAccountExists()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var exception = Assert.ThrowsAsync<VmsException>(() => sut.SignIn(TestEmail, TestPassword, Roles.SystemAdmin));

            //Assert
            exception.Code.ShouldBe(Codes.InvalidCredentials);
        }


        [Test]  
        public void SignIn_Always_ThrowsWhenPasswordIsIncorrect()
        {
            //Arrange
            var sut = CreateSut();
            _identityRepo.Setup(o => o.GetByEmailAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(_identity.Object));
            _passwordManager.Setup(o => o.IsPasswordCorrect(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
                .Returns(false);

            //Act
            var exception = Assert.ThrowsAsync<VmsException>(() => sut.SignIn(TestEmail, TestPassword, Roles.SystemAdmin));

            //Assert
            exception.Code.ShouldBe(Codes.InvalidCredentials);
        }

        [Test]
        public async Task SignIn_Always_ReturnsTokenIfSignInValid()
        {
            //Arrange
            var sut = CreateSut();
            _jwtManager.Setup(o => o.CreateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("TOKEN");
            _identityRepo.Setup(o => o.GetByEmailAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(_identity.Object));
            _passwordManager.Setup(o => o.IsPasswordCorrect(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
                .Returns(true);

            //Act
            var result = await sut.SignIn(TestEmail, TestPassword, Roles.SystemAdmin);

            //Assert
            result.ShouldNotBeNull();
        }

        [Test]
        public async Task SignIn_Always_CreatesRefreshTokenIfSignInValid()
        {
            //Arrange
            var sut = CreateSut();
            _jwtManager.Setup(o => o.CreateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("TOKEN");
            _identityRepo.Setup(o => o.GetByEmailAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(_identity.Object));
            _passwordManager.Setup(o => o.IsPasswordCorrect(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
                .Returns(true);

            //Act
            await sut.SignIn(TestEmail, TestPassword, Roles.SystemAdmin);

            //Assert
            _refreshTokenService.Verify(o => o.CreateRefreshToken(_identity.Object.Email));
        }

        [Test]
        public void CreateAdmin_Always_ThrowsWhenEmailInUse()
        {
            //Arrange
            var sut = CreateSut();

            //Act   
            var exception = Assert.ThrowsAsync<VmsException>(() => sut.CreateAdmin(TestInUseEmail));

            //Assert
            exception.Code.ShouldBe(Codes.EmailInUse);
        }

        [Test]
        public async Task CreateAdmin_Always_StoresPendingIdentityIfEmailNotInUse()
        {
            //Arrange
            var sut = CreateSut();
                
            //Act
            await sut.CreateAdmin(TestEmail);

            //Assert
            //TODO: verify call to add repo method.
            _pendingRepo.Verify(o => o.AddAsync(It.IsAny<PendingIdentity>()));
        }

        [Test]
        public async Task CreateAdmin_Always_PublishesSendEmailMessage()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.CreateAdmin(TestEmail);

            //Assert
            _serviceBusMessagePublisher.Verify(o => o.PublishEvent(It.IsAny<PendingAdminCreated>(), It.IsAny<IRequestInfo>()));
        }

        [Test]
        public void CompleteAdmin_Always_ThrowsIfCodeInvalid()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var exception = Assert.ThrowsAsync<VmsException>(() =>
                sut.CompleteAdmin(_invalidCode, TestPassword, TestPassword, TestEmail));

            //Assert
            exception.Code.ShouldBe(Codes.InvalidCredentials);
        }

        [Test]
        public void CompleteAdmin_Always_ThrowsIfPasswordsDoNotMatch()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var exception = Assert.ThrowsAsync<VmsException>(() =>
                sut.CompleteAdmin(_validCode, TestPassword, "A", TestEmail));

            //Assert
            exception.Code.ShouldBe(Codes.InvalidCredentials);
        }

        [Test]
        public void CompleteAdmin_Always_ThrowsIfEmailIsIncorrect()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var exception = Assert.ThrowsAsync<VmsException>(() =>
                sut.CompleteAdmin(_validCode, TestPassword, TestPassword, TestInValidEmail));

            //Assert
            exception.Code.ShouldBe(Codes.InvalidCredentials);
        }

        [Test]
        public async Task CompleteAdmin_Always_CreatesAdminIfValid()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.CompleteAdmin(_validCode, TestPassword, TestPassword, TestEmail);

            //Assert
            _identityRepo.Verify(o => o.AddAsync(It.IsAny<Domain.Identity>()));
        }

        [Test]
        public void CreateBusinessAdmin_Always_ThrowsIfEmailInUse()
        {
            //Arrange
            var sut = CreateSut();

            //Act   
            var exception = Assert.ThrowsAsync<VmsException>(() => sut.CreateBusinessAdmin(TestInUseEmail, Guid.NewGuid()));

            //Assert
            exception.Code.ShouldBe(Codes.EmailInUse);
        }

        [Test]
        public void CreateBusinessAdmin_Always_ThrowsIfBusinessNotFound()
        {
            //Arrange
            var sut = CreateSut();
            _businessRepository.Setup(o => o.ContainsBusinessAsync(It.IsAny<Guid>())).Returns(Task.FromResult(false));

            //Act   
            var exception = Assert.ThrowsAsync<VmsException>(() => sut.CreateBusinessAdmin(TestEmail, Guid.NewGuid()));

            //Assert
            exception.Code.ShouldBe(Codes.BusinessNotFound);
        }

        [Test]
        public async Task CreateBusinessAdmin_Always_StoresPendingIdentityWhenValid()
        {
            //Arrange
            var sut = CreateSut();

            //Act   
            await sut.CreateBusinessAdmin(TestEmail, Guid.NewGuid());

            //Assert
            _pendingRepo.Verify(o => o.AddAsync(It.Is<PendingIdentity>(pi => pi.Email == TestEmail && pi.Role == Roles.BusinessAdmin)));
        }

        [Test]
        public async Task CreateBusinessAdmin_Always_SendsCreatedMessageWhenValid()
        {
            //Arrange
            var sut = CreateSut();

            //Act   
            await sut.CreateBusinessAdmin(TestEmail, Guid.NewGuid());

            //Assert
            _serviceBusMessagePublisher.Verify(o => o.PublishEvent(It.Is<PendingBusinessAdminCreated>(p => p.Email == TestEmail), It.IsAny<RequestInfo>()));
        }

        public IAdminIdentityService CreateSut() => 
            new AdminIdentityService(_serviceBusMessagePublisher.Object, _identityRepo.Object, _pendingRepo.Object, _passwordManager.Object ,_jwtManager.Object, LoggerMock.CreateVms<AdminIdentityService>(), _refreshTokenService.Object, _businessRepository.Object);
    }
}
