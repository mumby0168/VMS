using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Services.Identity.Domain;
using Services.Identity.Repositorys;
using Services.Identity.Services;
using Shouldly;

namespace Services.Identity.Tests.Services
{
    class RefreshTokenServiceTests
    {

        private Mock<IRefreshTokenRepository> _repository;
        private const string TestEmail = "test@test.com";

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IRefreshTokenRepository>();
        }

        [Test]
        public async Task CreateRefreshToken_Always_SavesRefreshTokenToStorage()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.CreateRefreshToken(TestEmail);

            //Assert
            _repository.Verify(o => o.AddAsync(It.Is<RefreshToken>(rt => rt.Email == TestEmail))) ;
        }

        [Test]
        public async Task CreateRefreshToken_Always_ReturnsToken()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var result = await sut.CreateRefreshToken(TestEmail);

            //Assert
            result.ShouldNotBeNullOrEmpty();
        }

        public RefreshTokenService CreateSut() => new RefreshTokenService(_repository.Object);
    }
}
