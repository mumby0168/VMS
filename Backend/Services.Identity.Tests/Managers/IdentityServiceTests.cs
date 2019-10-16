using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Services.Common.Exceptions;
using Services.Common.Jwt;
using Services.Identity.Services;
using Shouldly;

namespace Services.Identity.Tests.Managers
{
    public class IdentityServiceTests
    {
        private const string TestEmail = "test@test.com";
        private const string TestInUseEmail = "test1@test.com";
        private const string TestInValidEmail = "test12@test.com";
        private const string TestPassword = "Test123";
        private readonly Guid _invalidCode = Guid.NewGuid();
        private readonly Guid _validCode = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {

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

            //Act
            var exception = Assert.ThrowsAsync<VmsException>(() => sut.SignIn(TestEmail, TestPassword, Roles.SystemAdmin));

            //Assert
            exception.Code.ShouldBe(Codes.InvalidCredentials);
        }

        public async Task SignIn_Always_ReturnsTokenIfSignInValid()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var result = await sut.SignIn(TestEmail, TestPassword, Roles.SystemAdmin);

            //Assert
            result.ShouldNotBeEmpty();
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
        }

        [Test]
        public async Task CreateAdmin_Always_PublishesSendEmailMessage()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.CreateAdmin(TestEmail);

            //Assert
            //TODO: verify email sent message with correct subject.
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
            exception.Code.ShouldBe(Codes.InvalidCode);
        }

        [Test]
        public void CompleteAdmin_Always_ThrowsIfPasswordsDoNotMatch()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var exception = Assert.ThrowsAsync<VmsException>(() =>
                sut.CompleteAdmin(_invalidCode, TestPassword, "A", TestEmail));

            //Assert
            exception.Code.ShouldBe(Codes.InvalidPasswords);
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
            exception.Code.ShouldBe(Codes.InvalidEmail);
        }

        [Test]
        public async Task CompleteAdmin_Always_CreatesAdminIfValid()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            await sut.CompleteAdmin(_validCode, TestPassword, TestPassword, TestEmail);

            //Assert
            //TODO: verify call to add method on identity repo.
        }

        public IIdentityService CreateSut() => new IdentityService();
    }
}
